using OauthExample.MyWebApi.Models;

namespace OauthExample.MyWebApi.Services
{
    public interface IAuthenticationService
    {
        TokenResponse RequestAccessToken(TokenRequest request);
        bool IsAccessTokenValid(string token);
    }
}
