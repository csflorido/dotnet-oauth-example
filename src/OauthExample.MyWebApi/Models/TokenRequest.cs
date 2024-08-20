using System.Text.Json.Serialization;

namespace OauthExample.MyWebApi.Models
{
    public class TokenRequest
    {
        [JsonPropertyName("grant_type")]
        public string? GrantType { get; set; }

        [JsonPropertyName("client_id")]
        public string? ClientId { get; set; }

        [JsonPropertyName("client_secret")]
        public string? ClientSecret { get; set; }

        [JsonPropertyName("scope")]
        public string? Scope { get; set; }
    }
}
