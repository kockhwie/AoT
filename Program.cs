using AOT.Components;
using AOT.Services;
using System.Globalization;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);
var isRender = !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("RENDER"));

// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddScoped<AppLocalizationService>();
builder.Services.AddScoped<LanguageSwitchService>();
builder.Services.AddSingleton<FactionPollService>(); // Add the FactionPollService as a singleton

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Render free-tier containers are ephemeral, so use an in-memory provider there to avoid
// file-system key storage warnings and stale protected payloads after restarts.
if (isRender)
{
    builder.Services.AddSingleton<IDataProtectionProvider>(sp =>
        new EphemeralDataProtectionProvider(sp.GetRequiredService<ILoggerFactory>()));
}
else
{
    // Persist keys locally during development so browser sessions and antiforgery tokens survive reloads.
    builder.Services.AddDataProtection()
        .SetApplicationName("AOT")
        .SetDefaultKeyLifetime(TimeSpan.FromDays(3650))
        .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "DataProtectionKeys")));
}

// Antiforgery is required for the interactive server endpoint metadata used by this app.
// Keep it enabled so Blazor's endpoint pipeline stays happy.
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.SuppressXFrameOptionsHeader = false;
});

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
if (!isRender)
{
    app.UseHttpsRedirection();
}

if (isRender)
{
    app.Use(async (context, next) =>
    {
        try
        {
            await next();
        }
        catch (AntiforgeryValidationException)
        {
            if (context.Response.HasStarted)
            {
                throw;
            }

            var antiforgeryOptions = context.RequestServices.GetRequiredService<IOptions<AntiforgeryOptions>>().Value;
            var cookieOptions = antiforgeryOptions.Cookie.Build(context);

            if (!string.IsNullOrWhiteSpace(cookieOptions.Name))
            {
                context.Response.Cookies.Delete(cookieOptions.Name, cookieOptions);
            }

            context.Response.Redirect(context.Request.PathBase + context.Request.Path + context.Request.QueryString);
        }
    });
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
