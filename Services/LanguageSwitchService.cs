using System.Globalization;

namespace AOT.Services;

/// <summary>
/// Centralized service for managing language switching across the application.
/// This service eliminates the need to duplicate language-change logic on every page
/// by providing a single source of truth for the active language state.
/// </summary>
public class LanguageSwitchService
{
    private string _activeLang = "zh";

    /// <summary>
    /// Gets or sets the currently active language code (e.g., "en", "zh", "ja").
    /// </summary>
    public string ActiveLang
    {
        get => _activeLang;
        private set => _activeLang = value;
    }

    /// <summary>
    /// Event raised whenever the language is changed.
    /// Subscribe to this in Razor pages to react to language changes without code duplication.
    /// </summary>
    public event Action<string>? OnLanguageChanged;

    /// <summary>
    /// The valid URL locale prefixes supported by this application.
    /// </summary>
    public static readonly string[] ValidLangs = { "zh", "en", "ja" };

    /// <summary>
    /// Initializes the service with the given language.
    /// Called during app startup to set the initial language.
    /// </summary>
    /// <param name="languageCode">The initial language code (e.g., "en", "zh", "ja")</param>
    public void Initialize(string languageCode)
    {
        // Normalize ja/jp alias
        var normalized = Normalize(languageCode);

        // Silently ignore invalid codes — fall back to current lang
        if (!ValidLangs.Contains(normalized)) return;

        // Force-update even if same lang (handles page navigation to a lang URL)
        _activeLang = normalized;
        UpdateCultureContext(normalized);
        OnLanguageChanged?.Invoke(normalized);
    }

    /// <summary>
    /// Changes the active language and updates the culture context.
    /// This is the single point through which all language changes flow,
    /// ensuring consistent behavior across the application.
    /// </summary>
    /// <param name="languageCode">The target language code</param>
    public void ChangeLanguage(string languageCode)
    {
        var normalized = Normalize(languageCode);
        if (_activeLang == normalized) return;

        _activeLang = normalized;
        UpdateCultureContext(normalized);
        OnLanguageChanged?.Invoke(normalized);
    }

    /// <summary>
    /// Returns the URL prefix for a language (e.g. "zh" → "/zh", "en" → "/en").
    /// </summary>
    public static string GetLangPrefix(string lang) => $"/{Normalize(lang)}";

    /// <summary>
    /// Given a current relative URL (e.g. "/en/manga/1"), returns the equivalent URL
    /// for the target language (e.g. "/zh/manga/1"). Handles the bare "/" root.
    /// </summary>
    public static string GetLocalizedUrl(string currentRelativeUrl, string targetLang)
    {
        var target = Normalize(targetLang);
        var targetPrefix = GetLangPrefix(target);

        // Strip existing lang prefix if present (/zh/..., /en/..., /ja/...)
        string path = currentRelativeUrl;
        foreach (var lang in ValidLangs)
        {
            var prefix = $"/{lang}";
            if (path == prefix || path == prefix + "/")
            {
                path = "/";
                break;
            }
            if (path.StartsWith(prefix + "/"))
            {
                path = path.Substring(prefix.Length); // keeps leading /
                break;
            }
        }

        // path is now the language-neutral path (e.g. "/", "/manga", "/manga/1")
        return path == "/" ? targetPrefix + "/" : targetPrefix + path;
    }

    /// <summary>
    /// Updates the current thread's culture to match the language setting.
    /// </summary>
    private static void UpdateCultureContext(string languageCode)
    {
        var cultureInfo = new CultureInfo(languageCode);
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
    }

    /// <summary>
    /// Gets the display name for a language code.
    /// </summary>
    public string GetLanguageDisplayName(string languageCode) =>
        Normalize(languageCode) switch
        {
            "en" => "English",
            "zh" => "汉语",
            "ja" => "日本語",
            _ => languageCode
        };

    /// <summary>
    /// Gets the available language options.
    /// </summary>
    public IEnumerable<string> GetAvailableLanguages() => ValidLangs;

    /// <summary>
    /// Normalizes "jp" → "ja" for backwards compatibility.
    /// </summary>
    public static string Normalize(string lang) => lang == "jp" ? "ja" : lang;
}
