using AOT.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Resources;

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

    /// <summary>
    /// Sample:
    ///     <p>@LocalizationService.GetStringWithLang("Nav_LeviAckerman", "en") / @LocalizationService.GetStringWithLang("Nav_LeviAckerman", "zh")</p>
    /// </summary>
    public string GetStringWithLang(string key, string? cultureName = null)
    {
        if (string.IsNullOrEmpty(cultureName))
        {
            return GetText(key); // Fallback to current behavior
        }

        var culture = new CultureInfo(cultureName);

        // Bypasses the localizer entirely and directly queries the underlying .resx
        // Replace 'SharedResources' with the actual class name of your main .resx file
        ResourceManager rm = AppStrings.ResourceManager;

        return rm.GetString(key, culture) ?? GetText(key);
    }

    /// <summary>
    /// Sample:
    ///     <p>The author is: @LocalizationService.GetFlexibleName("Nav_LeviAckerman")</p>
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public MarkupString GetFlexibleName(string key)
    {

        // FIX: Read your active service state instead of the server thread culture
        // Normalize "jp" to "ja" for .NET's ResourceManager
        string currentLang = ActiveLang == "jp" ? "ja" : ActiveLang;
 
        // string currentLang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        ResourceManager rm = AppStrings.ResourceManager;

        string textEn = rm.GetString(key, new CultureInfo("en")) ?? "";
        string textZh = rm.GetString(key, new CultureInfo("zh")) ?? "";
        string textJa = rm.GetString(key, new CultureInfo("ja")) ?? "";

        string htmlResult;

        switch (currentLang)
        {
            case "zh":
                htmlResult = !string.IsNullOrEmpty(textEn)
                    ? $"<strong class=\"main-name\">{textZh}</strong> <span class=\"alt-name\">({textEn})</span>"
                    : $"<strong class=\"main-name\">{textZh}</strong>";
                break;

            case "ja":
                htmlResult = !string.IsNullOrEmpty(textEn)
                    ? $"<strong class=\"main-name\">{textJa}</strong> <span class=\"alt-name\">({textEn})</span>"
                    : $"<strong class=\"main-name\">{textJa}</strong>";
                break;

            case "en":
            default:
                string altText = !string.IsNullOrEmpty(textZh) ? textZh : textJa;
                htmlResult = !string.IsNullOrEmpty(altText)
                    ? $"<strong class=\"main-name\">{textEn}</strong> <span class=\"alt-name\">({altText})</span>"
                    : $"<strong class=\"main-name\">{textEn}</strong>";
                break;
        }


        // Returns a native Blazor safe HTML string
        return (MarkupString)htmlResult;
    }

}
