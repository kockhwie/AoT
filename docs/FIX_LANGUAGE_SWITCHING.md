## Why /manga Page Wasn't Switching Language - Fix Applied

### The Problem

When you clicked on the language switcher buttons (EN/JP/CN), the page didn't immediately update its content to the new language. This was because:

1. **Missing Event Subscription**: Pages like `/manga`, `/manga/{volume}` were injecting `LocalizationService` but NOT subscribing to `LanguageSwitchService.OnLanguageChanged` event
2. **No Re-render Trigger**: Without subscribing, when `LanguageSwitchService` notified that the language changed, the page had no way to respond
3. **Stale UI**: The strings stayed in the old language because `StateHasChanged()` was never called

### Root Cause Diagram

```
User clicks language button
   ↓
MainLayout.ChangeLanguage() calls LanguageSwitcher.ChangeLanguage()
   ↓
LanguageSwitcher.OnLanguageChanged event fires
   ↓
But /manga page is NOT listening! ❌
   ↓
Page doesn't know language changed
   ↓
Page never calls StateHasChanged()
   ↓
UI stays in old language 😞
```

### Solution Applied

I updated the following pages to **subscribe to language change events**:

#### 1. **Components/Pages/Manga.razor** ✅
- Added `@inject LanguageSwitchService LanguageSwitcher`
- Added `@implements IDisposable`
- Added event subscription in `OnInitialized()`
- Added cleanup in `Dispose()`

```csharp
@code {
	protected override void OnInitialized()
	{
		// Subscribe to language changes to trigger immediate UI update
		LanguageSwitcher.OnLanguageChanged += HandleLanguageChanged;
	}

	private void HandleLanguageChanged(string newLanguage)
	{
		// Trigger re-render when language changes
		StateHasChanged();
	}

	public void Dispose()
	{
		// Unsubscribe to prevent memory leaks
		LanguageSwitcher.OnLanguageChanged -= HandleLanguageChanged;
	}
}
```

#### 2. **Components/Pages/MangaVolume.razor** ✅
- Same fix as Manga.razor
- Added `@inject LanguageSwitchService LanguageSwitcher`
- Added `@implements IDisposable`
- Subscribed to `OnLanguageChanged` event

#### 3. **Components/Pages/Home.razor** ✅
- Already had the infrastructure in place (uses `CurrentLang` cascading parameter)
- Added `@inject LanguageSwitchService LanguageSwitcher`
- Added event subscription
- Event handler calls `BuildMangaSlides()` to refresh slide content with new language quotes

### How It Works Now

```
User clicks language button (e.g., "中文")
   ↓
MainLayout.ChangeLanguage("zh") → LanguageSwitcher.ChangeLanguage("zh")
   ↓
LanguageSwitcher.OnLanguageChanged event fires
   ↓
Manga page HandleLanguageChanged() is called
   ↓
Page calls StateHasChanged()
   ↓
Blazor re-renders component
   ↓
LocalizationService now returns strings in new language
   ↓
UI immediately updates with new language ✅
```

### Pattern for Future Pages

If you add new pages that display language-sensitive content, use this pattern:

```razor
@page "/mynewpage"
@inject AppLocalizationService LocalizationService
@inject LanguageSwitchService LanguageSwitcher
@implements IDisposable

<!-- Your UI here -->

@code {
	protected override void OnInitialized()
	{
		LanguageSwitcher.OnLanguageChanged += HandleLanguageChanged;
	}

	private void HandleLanguageChanged(string newLanguage)
	{
		StateHasChanged();
	}

	public void Dispose()
	{
		LanguageSwitcher.OnLanguageChanged -= HandleLanguageChanged;
	}
}
```

### Why This Works

1. **Event-Driven**: The service notifies subscribers when language changes (pub/sub pattern)
2. **Reactive Updates**: Pages react immediately by calling `StateHasChanged()`
3. **Clean Memory**: We unsubscribe in `Dispose()` to prevent memory leaks
4. **No Polling**: No wasteful background checks needed

### Testing the Fix

After building, test the language switching:

1. Go to `/manga` page
2. Click a language button (EN/JP/CN) in the top navbar
3. ✅ The page content should **immediately** update to the new language

This now works on:
- ✅ Home page (/)
- ✅ Manga listing page (/manga)
- ✅ Individual volume pages (/manga/{volume})
