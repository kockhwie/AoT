using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AOT.Services;

/// <summary>
/// Tracks faction poll votes using Upstash Redis REST API with Hash dynamic key storage.
/// Allows adding unlimited factions without schema/code change.
/// Gracefully falls back to thread-safe in-memory storage if Upstash is unconfigured or unreachable.
/// </summary>
public class FactionPollService
{
    private readonly IHttpClientFactory _factory;
    private readonly string? _restUrl;
    private readonly string? _restToken;
    private const string HashKey = "poll:aot_faction_votes";

    private readonly ConcurrentDictionary<string, long> _fallbackVotes = new(StringComparer.OrdinalIgnoreCase);

    public FactionPollService(IHttpClientFactory factory, IConfiguration config)
    {
        _factory = factory;
        _restUrl = config["UPSTASH_REDIS_REST_URL"] ?? config["Upstash:RestUrl"];
        _restToken = config["UPSTASH_REDIS_REST_TOKEN"] ?? config["Upstash:RestToken"];
    }

    public bool IsConfigured => !string.IsNullOrWhiteSpace(_restUrl) && !string.IsNullOrWhiteSpace(_restToken);

    public async Task<Dictionary<string, long>> GetAllVotesAsync()
    {
        if (!IsConfigured)
        {
            return new Dictionary<string, long>(_fallbackVotes, StringComparer.OrdinalIgnoreCase);
        }

        try
        {
            var client = CreateHttpClient();
            // Upstash HGETALL endpoint returns JSON: { "result": ["marley", "10", "paradis", "12"] } or object map
            var response = await client.GetAsync($"/hgetall/{HashKey}");
            if (!response.IsSuccessStatusCode)
            {
                return new Dictionary<string, long>(_fallbackVotes, StringComparer.OrdinalIgnoreCase);
            }

            using var stream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);
            var root = doc.RootElement;
            if (!root.TryGetProperty("result", out var resultElement))
            {
                return new Dictionary<string, long>(_fallbackVotes, StringComparer.OrdinalIgnoreCase);
            }

            var votes = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);

            if (resultElement.ValueKind == JsonValueKind.Array)
            {
                var items = resultElement.EnumerateArray().ToList();
                for (int i = 0; i < items.Count - 1; i += 2)
                {
                    var faction = items[i].GetString();
                    if (!string.IsNullOrEmpty(faction) && long.TryParse(items[i + 1].GetString(), out var count))
                    {
                        votes[faction] = count;
                    }
                }
            }
            else if (resultElement.ValueKind == JsonValueKind.Object)
            {
                foreach (var prop in resultElement.EnumerateObject())
                {
                    if (long.TryParse(prop.Value.GetString() ?? prop.Value.GetRawText(), out var count))
                    {
                        votes[prop.Name] = count;
                    }
                }
            }

            return votes;
        }
        catch
        {
            return new Dictionary<string, long>(_fallbackVotes, StringComparer.OrdinalIgnoreCase);
        }
    }

    public async Task<long> VoteAsync(string faction)
    {
        if (string.IsNullOrWhiteSpace(faction)) return 0;

        faction = faction.Trim().ToLowerInvariant();

        if (!IsConfigured)
        {
            return _fallbackVotes.AddOrUpdate(faction, 1, (_, current) => current + 1);
        }

        try
        {
            var client = CreateHttpClient();
            // HINCRBY poll:aot_faction_votes {faction} 1
            var response = await client.PostAsync($"/hincrby/{HashKey}/{faction}/1", null);
            if (!response.IsSuccessStatusCode)
            {
                return _fallbackVotes.AddOrUpdate(faction, 1, (_, current) => current + 1);
            }

            using var stream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);
            if (doc.RootElement.TryGetProperty("result", out var resElement))
            {
                if (resElement.ValueKind == JsonValueKind.Number && resElement.TryGetInt64(out var count))
                {
                    return count;
                }
                if (long.TryParse(resElement.GetString() ?? resElement.GetRawText(), out var parsedCount))
                {
                    return parsedCount;
                }
            }

            return _fallbackVotes.AddOrUpdate(faction, 1, (_, current) => current + 1);
        }
        catch
        {
            return _fallbackVotes.AddOrUpdate(faction, 1, (_, current) => current + 1);
        }
    }

    private HttpClient CreateHttpClient()
    {
        var client = _factory.CreateClient("UpstashRedis");
        var baseUrl = _restUrl!.TrimEnd('/');
        client.BaseAddress = new Uri(baseUrl);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _restToken);
        return client;
    }
}