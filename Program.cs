using AOT.Components;
using AOT.Services;
using System.Globalization;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
 

// Load optional local appsettings secrets file if present
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: false);

// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddScoped<AppLocalizationService>();
builder.Services.AddScoped<LanguageSwitchService>();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<FactionPollService>(); // Add the FactionPollService as a singleton

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

ConfigureDataProtection(builder.Services, builder.Environment);
ConfigureAntiforgery(builder.Services);

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


// Ephemeral key storage means restarts invalidate old antiforgery cookies —
// recover from that instead of 500ing.
if (!PersistDataProtectionKeys(app.Environment))
{
    UseAntiforgeryRecovery(app);
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();


// ponytail: don't detect "is this Render" — env var names/values are outside your control and
// can change without notice. The thing that actually matters is "does this container have a
// persistent disk", and the safe default for any cloud host (Render, Fly, Railway, a future
// host you haven't picked yet) is no. Opt in explicitly if you ever attach real persistent
// storage. Upgrade path: set PERSIST_DP_KEYS=true once you have a real volume mounted.
static bool PersistDataProtectionKeys(IWebHostEnvironment environment)
{
    return environment.IsDevelopment()
        || Environment.GetEnvironmentVariable("PERSIST_DP_KEYS") == "true";
}

static void ConfigureDataProtection(IServiceCollection services, IWebHostEnvironment environment)
{
    if (!PersistDataProtectionKeys(environment))
    {
        services.AddSingleton<IDataProtectionProvider>(sp =>
            new EphemeralDataProtectionProvider(sp.GetRequiredService<ILoggerFactory>()));
        return;
    }

    services.AddDataProtection()
        .SetApplicationName("AOT")
        .SetDefaultKeyLifetime(TimeSpan.FromDays(3650))
        .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(environment.ContentRootPath, "DataProtectionKeys")));
}

static void ConfigureAntiforgery(IServiceCollection services)
{
    services.AddAntiforgery(options =>
    {
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.SuppressXFrameOptionsHeader = false;
    });
}

static void UseAntiforgeryRecovery(WebApplication app)
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
            var cookieName = antiforgeryOptions.Cookie.Name ?? ".AspNetCore.Antiforgery";

            if (!string.IsNullOrWhiteSpace(cookieName))
            {
                context.Response.Cookies.Delete(cookieName, cookieOptions);
            }

            context.Response.Redirect(context.Request.PathBase + context.Request.Path + context.Request.QueryString);
        }
    });
}
