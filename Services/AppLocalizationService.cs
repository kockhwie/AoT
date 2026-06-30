using System.Globalization;
using Microsoft.Extensions.Localization;

namespace AOT.Services;

/// <summary>
/// Provides localized string access using .NET's IStringLocalizer pattern.
/// This service abstracts the localization implementation, making it easy to switch
/// from resource files to database-driven localization in the future.
/// </summary>
public class AppLocalizationService
{
    private readonly IStringLocalizer _localizer;

    public string ActiveLang { get; set; } = "zh";

    public AppLocalizationService(IStringLocalizerFactory factory)
    {
        var assemblyName = typeof(AppLocalizationService).Assembly.GetName().Name;
        _localizer = factory.Create("AppStrings", assemblyName ?? "AOT");
    }

    private string GetText(string key)
    {
        var cultureCode = ActiveLang == "jp" ? "ja" : ActiveLang;
        var culture = new CultureInfo(cultureCode);
        
        var prevCulture = CultureInfo.CurrentCulture;
        var prevUiCulture = CultureInfo.CurrentUICulture;
        try
        {
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            return _localizer[key];
        }
        finally
        {
            CultureInfo.CurrentCulture = prevCulture;
            CultureInfo.CurrentUICulture = prevUiCulture;
        }
    }

    // Navigation
    public string NavCharacters => GetText("NavCharacters");
    public string NavExplore => GetText("NavExplore");
    public string NavLoreMaps => GetText("NavLoreMaps");
    public string NavMedia => GetText("NavMedia");
    public string NavNews => GetText("NavNews");
    public string NavFanZone => GetText("NavFanZone");

    // Sidebar
    public string SidebarTitle => GetText("SidebarTitle");
    public string SidebarDesc => GetText("SidebarDesc");
    public string SidebarBtn => GetText("SidebarBtn");

    // Faction
    public string FactionTitle => GetText("FactionTitle");
    public string FactionDesc => GetText("FactionDesc");
    public string FactionBtn1 => GetText("FactionBtn1");
    public string FactionBtn2 => GetText("FactionBtn2");

    // Hero Section
    public string HeroTag => GetText("HeroTag");
    public string HeroTitle => GetText("HeroTitle");
    public string HeroDesc => GetText("HeroDesc");
    public string BtnIntel => GetText("BtnIntel");
    public string BtnGallery => GetText("BtnGallery");

    // Section
    public string SectionTitle => GetText("SectionTitle");

    // Footer
    public string FooterCredit => GetText("FooterCredit");

    /// <summary>
    /// Generic method to get any resource string by key.
    /// This provides flexibility for dynamic or extra strings not defined as properties.
    /// </summary>
    /// <param name="key">The resource key</param>
    /// <returns>The localized string value</returns>
    public string GetString(string key)
    {
        return GetText(key);
    }
}
