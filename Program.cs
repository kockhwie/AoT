using AOT.Components;
using AOT.Services;
using System.Globalization;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddScoped<AppLocalizationService>();
builder.Services.AddScoped<LanguageSwitchService>();
builder.Services.AddSingleton<FactionPollService>(); // Add the FactionPollService as a singleton

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure Data Protection to persist keys securely.
// In Render, we can provide the key via the DATA_PROTECTION_KEY_XML environment variable to avoid committing it to GitHub.
var dpBuilder = builder.Services.AddDataProtection()
    .SetApplicationName("AOT")
    .SetDefaultKeyLifetime(TimeSpan.FromDays(3650)); // 10 years

var dpKeyXml = Environment.GetEnvironmentVariable("DATA_PROTECTION_KEY_XML");
if (!string.IsNullOrWhiteSpace(dpKeyXml) && CanParseXml(dpKeyXml))
{
    // Use the environment variable on Render
    dpBuilder.AddKeyManagementOptions(options =>
    {
        options.XmlRepository = new EnvironmentVariableXmlRepository("DATA_PROTECTION_KEY_XML");
    });
}
else
{
    // Fallback to the local file system for development or when the Render key is missing/invalid.
    dpBuilder.PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "DataProtectionKeys")));
}


var app = builder.Build();

// Configure supported cultures for localization
var supportedCultures = new[] { "zh", "en", "ja" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

// Render terminates TLS before traffic reaches the app, so redirecting here just adds noise.
if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("RENDER")))
{
    app.UseHttpsRedirection();
}


app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

static bool CanParseXml(string xml)
{
    try
    {
        System.Xml.Linq.XElement.Parse(xml.Trim());
        return true;
    }
    catch
    {
        return false;
    }
}
