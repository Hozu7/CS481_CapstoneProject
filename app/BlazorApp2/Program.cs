using BlazorApp2.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register WeatherService
builder.Services.AddScoped<WeatherService>();

// Register LogoutService
builder.Services.AddScoped<LogoutService>();

// Configure an authenticated HTTP client in program.cs
builder.Services.AddHttpClient("AuthenticatedClient",
    client => { client.BaseAddress = new Uri("https://localhost:7151/"); })
  .AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
})
.AddOpenIdConnect(options =>
{
    options.Authority = builder.Configuration["Cognito:Authority"];
    options.ClientId = builder.Configuration["Cognito:ClientId"];
    options.ClientSecret = builder.Configuration["Cognito:ClientSecret"];
    options.ResponseType = OpenIdConnectResponseType.Code; // Use authorization code flow
    options.SaveTokens = true;
    options.UseTokenLifetime = true;

    options.Events = new OpenIdConnectEvents
    {
        OnRedirectToIdentityProviderForSignOut = (context) =>
        {
            var clientId = builder.Configuration["Cognito:ClientId"];
            var logoutRedirectUri = builder.Configuration["Cognito:LogoutRedirectUri"];
            var cognitoDomain = builder.Configuration["Cognito:Domain"];

            if (!cognitoDomain.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                cognitoDomain = "https://" + cognitoDomain;
            }

            var logoutUri = $"{cognitoDomain}/logout?client_id={clientId}&logout_uri={Uri.EscapeDataString(logoutRedirectUri)}";

            context.Response.Redirect(logoutUri);
            context.HandleResponse();
            return Task.CompletedTask;
        }
    };
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
    options.Authority = $"https://cognito-idp.{builder.Configuration["Cognito:Region"]}.amazonaws.com/{builder.Configuration["Cognito:UserPoolId"]}";
    options.Audience = builder.Configuration["AWS:Cognito:ClientId"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Cognito:Authority"],
        ClockSkew = TimeSpan.FromMinutes(5),
    };
});

builder.Services.AddCors(policy =>
{
    policy.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddTransient<JwtAuthorizationMessageHandler>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseCors();
app.UseAuthentication();
app.UseAntiforgery();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapGet("/weatherforecast", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] (WeatherService weatherService) =>
{
    var forecasts = weatherService.GetForecasts();
    return Results.Ok(forecasts);
});

app.MapGet("/logout", async context =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    context.Response.Redirect("/");
});

app.Run();