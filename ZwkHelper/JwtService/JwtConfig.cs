namespace JwtService
{
    public class JwtConfig
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public int Expired { get; set; }
    }

}
