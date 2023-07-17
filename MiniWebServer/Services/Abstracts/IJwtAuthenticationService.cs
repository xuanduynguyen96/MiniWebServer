namespace MiniWebServer.Services.Abstracts
{
    public interface IJwtAuthenticationService
    {
        string GetToken(IDictionary<string, string> claimsDefinations);
    }
}
