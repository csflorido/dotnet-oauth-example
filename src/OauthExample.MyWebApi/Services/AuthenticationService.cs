using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OauthExample.MyWebApi.Config;
using OauthExample.MyWebApi.Exceptions;
using OauthExample.MyWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OauthExample.MyWebApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings _settings;

        public AuthenticationService(IOptions<AppSettings> settings) 
        {
            _settings = settings.Value;
        }

        public TokenResponse RequestAccessToken(TokenRequest request)
        {
            if(
                string.IsNullOrWhiteSpace(request.ClientId)
                || string.IsNullOrWhiteSpace(request.ClientSecret)
                || string.IsNullOrWhiteSpace(request.GrantType)
            )
            {
                throw new BadRequestException(new ProblemDetails
                {
                    Title = "invalid_request",
                    Detail = "The authorization grant type is not supported by the authorization server."
                });
            }

            if(request.GrantType != "client_credentials")
            {
                throw new BadRequestException(new ProblemDetails
                {
                    Title = "unsupported_grant_type",
                    Detail = "The authorization grant type is not supported by the authorization server."
                });
            }

            if (request.ClientSecret != _settings.ClientSecret && request.ClientSecret != _settings.ClientSecret)
            {
                throw new UnauthorizedException();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.SigningKey);
            var expires = DateTime.UtcNow.AddSeconds(_settings.ExpiresInSeconds);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, request.ClientId)
                }),
                Expires = expires,
                Issuer = _settings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return new TokenResponse
            {
                AccessToken = token,
                ExpiresIn = _settings.ExpiresInSeconds,
                ExpiresAt = (new DateTimeOffset(expires)).ToUnixTimeSeconds(),
                TokenType = "Bearer"
            };
        }

        public bool IsAccessTokenValid(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_settings.SigningKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
