namespace OauthExample.MyWebApi.Config
{
    public class AppSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SigningKey { get; set; }
        public string Issuer { get; set; }
        public int ExpiresInSeconds { get; set; }
    }
}
