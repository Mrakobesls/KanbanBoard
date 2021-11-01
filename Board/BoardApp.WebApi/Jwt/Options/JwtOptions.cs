namespace BoardApp.WebApi.Jwt.Options
{
    public class JwtOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpiryDurationSeconds { get; set; }
    }
}
