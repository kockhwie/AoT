namespace AOT.Services;

/// <summary>
/// Tracks faction poll votes in memory. Not persisted across app restarts —
/// ponytail: fine for a lightweight fan-site poll; Render's free tier
/// idle-restarts reset this to zero. Upgrade path if that matters: swap the
/// two ints for a call to an external KV store (e.g. Upstash Redis free tier).
/// Repeat-vote prevention is NOT this service's job — that's a client-side
/// localStorage flag in MainLayout.razor. This just counts.
/// </summary>
public class FactionPollService
{
    private int _marleyVotes;
    private int _paradisVotes;
    private readonly Lock _lock = new();

    public int MarleyVotes => _marleyVotes;
    public int ParadisVotes => _paradisVotes;
    public int TotalVotes => _marleyVotes + _paradisVotes;

    public void Vote(string faction)
    {
        lock (_lock)
        {
            if (faction == "marley") _marleyVotes++;
            else if (faction == "paradis") _paradisVotes++;
        }
    }
}