using Microsoft.AspNetCore.Components;

public class LogoutService
{
    private readonly NavigationManager _navigationManager;
    private readonly IConfiguration _configuration;

    public LogoutService(NavigationManager navigationManager, IConfiguration configuration)
    {
        _navigationManager = navigationManager;
        _configuration = configuration;
    }

    public void Logout()
    {
        var clientId = _configuration["Cognito:ClientId"];
        var logoutRedirectUri = _configuration["Cognito:LogoutRedirectUri"];
        var cognitoDomain = _configuration["Cognito:Domain"];

        if (!cognitoDomain.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            cognitoDomain = "https://" + cognitoDomain;
        }

        var logoutUrl = $"{cognitoDomain}/logout?client_id={clientId}&logout_uri={Uri.EscapeDataString(logoutRedirectUri)}";
        _navigationManager.NavigateTo(logoutUrl, forceLoad: true);
    }
}