namespace Bat.AspNetCore
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Encryptionkey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
        public int RefreshTokenExpirationMinutes { get; set; }
    }
}