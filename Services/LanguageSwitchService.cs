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
    /// Gets or sets the currently active language code (e.g., "en", "zh", "jp").
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
    /// Initializes the service with the given language.
    /// Called during app startup to set the initial language.
    /// </summary>
    /// <param name="languageCode">The initial language code (e.g., "en", "zh", "jp")</param>
    public void Initialize(string languageCode)
    {
        ChangeLanguage(languageCode);
    }

    /// <summary>
    /// Changes the active language and updates the culture context.
    /// This is the single point through which all language changes flow,
    /// ensuring consistent behavior across the application.
    /// </summary>
    /// <param name="languageCode">The target language code</param>
    public void ChangeLanguage(string languageCode)
    {
        if (_activeLang == languageCode)
        {
            return; // No change needed
        }

        _activeLang = languageCode;

        // Update culture context for .NET localization
        UpdateCultureContext(languageCode);

        // Notify all subscribers of the language change
        OnLanguageChanged?.Invoke(languageCode);
    }

    /// <summary>
    /// Updates the current thread's culture to match the language setting.
    /// This single method replaces the repetitive culture update code that was scattered across pages.
    /// </summary>
    /// <param name="languageCode">The language code to apply</param>
    private void UpdateCultureContext(string languageCode)
    {
        // Normalize "jp" to "ja" for .NET's CultureInfo
        string cultureCode = languageCode == "jp" ? "ja" : languageCode;
        var cultureInfo = new CultureInfo(cultureCode);

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
    }

    /// <summary>
    /// Gets the display name for a language code.
    /// Useful for UI labels in language switchers.
    /// </summary>
    /// <param name="languageCode">The language code</param>
    /// <returns>The display name (e.g., "English", "中文", "日本語")</returns>
    public string GetLanguageDisplayName(string languageCode)
    {
        return languageCode switch
        {
            "en" => "English",
            "zh" => "汉语",
            "jp" => "日本語",
            _ => languageCode
        };
    }

    /// <summary>
    /// Gets the available language options.
    /// </summary>
    /// <returns>List of available language codes</returns>
    public IEnumerable<string> GetAvailableLanguages()
    {
        return new[] { "en", "zh", "jp" };
    }
}
