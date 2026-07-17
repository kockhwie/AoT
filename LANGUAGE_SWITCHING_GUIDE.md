## DRY Language Switching Implementation Guide

### Problem Solved
Previously, every Razor page that needed to react to language changes had to duplicate the `ChangeLanguage()` method and culture update logic. This violated the DRY (Don't Repeat Yourself) principle.

### Solution Architecture

The solution uses a **centralized service with event-driven updates**:

1. **LanguageSwitchService** - Manages language state centrally
2. **Event Pattern** - Pages subscribe to `OnLanguageChanged` event
3. **Scoped Dependency Injection** - Service is available to all components

---

## How to Use in Child Pages

### Example 1: Simple Page That Uses Current Language (No Subscription Needed)

```razor
@page "/characters"
@inject LanguageSwitchService LanguageSwitcher
@inject AppLocalizationService LocalizationService

<h1>@LocalizationService.NavCharacters</h1>
<p>Current language: @LanguageSwitcher.ActiveLang</p>
```

The page automatically gets the correct strings from `LocalizationService` because `MainLayout` already manages the global language state.

---

### Example 2: Page That Reacts to Language Changes (With Event Subscription)

If your page has dynamic content that must update when language changes:

```razor
@page "/titans"
@implements IDisposable
@inject LanguageSwitchService LanguageSwitcher
@inject AppLocalizationService LocalizationService

<h1>@LocalizationService.NavTitans</h1>

@if (_titanList != null)
{
	@foreach (var titan in _titanList)
	{
		<div>
			<h2>@titan.Name</h2>
			<p>@titan.Description</p>
		</div>
	}
}

@code {
	private List<TitanInfo>? _titanList;

	protected override void OnInitialized()
	{
		// Subscribe to language changes
		LanguageSwitcher.OnLanguageChanged += HandleLanguageChanged;

		// Load initial data
		LoadTitans();
	}

	private void HandleLanguageChanged(string newLanguage)
	{
		// Reload or update your data when language changes
		LoadTitans();
		StateHasChanged();
	}

	private void LoadTitans()
	{
		// Your data loading logic only happens ONCE per page
		// No need to duplicate language switching code!
		_titanList = GetTitansByLanguage(LanguageSwitcher.ActiveLang);
	}

	// Clean up: Unsubscribe from events
	public void Dispose()
	{
		LanguageSwitcher.OnLanguageChanged -= HandleLanguageChanged;
	}
}
```

---

### Example 3: Component That Uses Language Info

```razor
<!-- Components/CharacterCard.razor -->
@inject LanguageSwitchService LanguageSwitcher

<div class="character-card">
	<h3>@Character.GetLocalizedName(LanguageSwitcher.ActiveLang)</h3>
	<p>@Character.GetLocalizedDescription(LanguageSwitcher.ActiveLang)</p>
</div>

@code {
	[Parameter]
	public Character Character { get; set; } = null!;
}
```

---

## Available LanguageSwitchService Methods

```csharp
// Get the active language
string currentLang = LanguageSwitcher.ActiveLang;

// Change language (not needed on child pages usually)
LanguageSwitcher.ChangeLanguage("en");

// Get display name for a language
string displayName = LanguageSwitcher.GetLanguageDisplayName("zh"); // Returns "汉语"

// Get all available languages
var languages = LanguageSwitcher.GetAvailableLanguages(); // Returns ["en", "zh", "jp"]

// Subscribe to changes
LanguageSwitcher.OnLanguageChanged += (lang) =>
{
	Console.WriteLine($"Language changed to: {lang}");
};
```

---

## Key Benefits

✅ **No Code Duplication** - Language switching logic is in ONE place  
✅ **Event-Driven** - Pages only update when language actually changes  
✅ **Clean Architecture** - Separation of concerns  
✅ **Easy to Test** - Service is mockable and testable  
✅ **Scalable** - Add more pages without adding logic  
✅ **Future-Proof** - Easy to add persistence (localStorage, database, etc.)  

---

## Common Patterns

### Pattern 1: Cache Data by Language
```csharp
private Dictionary<string, List<Titan>> _titanCache = new();

private void LoadTitans()
{
	string lang = LanguageSwitcher.ActiveLang;

	if (!_titanCache.ContainsKey(lang))
	{
		_titanCache[lang] = FetchTitansByLanguage(lang);
	}

	_titanList = _titanCache[lang];
}
```

### Pattern 2: Format Numbers/Dates by Language
```csharp
private string FormatDate(DateTime date)
{
	var culture = new CultureInfo(LanguageSwitcher.ActiveLang == "jp" ? "ja" : LanguageSwitcher.ActiveLang);
	return date.ToString("D", culture);
}
```

---

## What Changed in MainLayout

Before: Language logic was duplicated on every page that needed it
After: MainLayout handles initialization and event dispatch

**Old Code (Repeated on Every Page):**
```csharp
private void ChangeLanguage(string targetLang)
{
	_activeLang = targetLang;
	LocalizationService.ActiveLang = targetLang;
	string cultureCode = targetLang == "jp" ? "ja" : targetLang;
	var cultureInfo = new System.Globalization.CultureInfo(cultureCode);
	System.Globalization.CultureInfo.CurrentCulture = cultureInfo;
	System.Globalization.CultureInfo.CurrentUICulture = cultureInfo;
	StateHasChanged(); 
}
```

**New Code (In Service Only):**
The `LanguageSwitchService.ChangeLanguage()` has this logic once, reused everywhere.

---

## Migration from Old Pattern

If you have existing pages with duplicated language code:

1. Remove the local `_activeLang` field
2. Remove the `ChangeLanguage()` method
3. Remove the `OnInitialized()` override
4. Replace manual language switches with `@onclick='() => LanguageSwitcher.ChangeLanguage("en")'`
5. If you need reactive updates, add event subscription as shown in Example 2

That's it! Your page now follows DRY principles.

---

## Future Enhancements

The service makes it easy to add:

- **localStorage Persistence**: Save language preference
- **URL-based Language**: Handle `/en/page` vs `/zh/page`
- **Automatic Detection**: Detect browser language
- **Database Localization**: Load strings from a database instead of .resx
- **RTL Support**: Add direction switching for languages like Arabic

All without changing a single page!
