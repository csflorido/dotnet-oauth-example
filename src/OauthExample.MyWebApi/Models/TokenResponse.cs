using System.Text.Json.Serialization;

namespace OauthExample.MyWebApi.Models
{
    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }
        
        [JsonPropertyName("expires_in")]
        public int? ExpiresIn { get; set; }
        
        [JsonPropertyName("expires_at")]
        public long? ExpiresAt { get; set; }
        
        [JsonPropertyName("scope")]
        public string? Scope { get; set; }

    }
}
