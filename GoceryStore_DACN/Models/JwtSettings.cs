namespace GoceryStore_DACN.Models
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpiryInMinutes { get; set; } = 0;

        public void LogValues()
        {
            Console.WriteLine($"SecretKey: {SecretKey}");
            Console.WriteLine($"Issuer: {Issuer}");
            Console.WriteLine($"Audience: {Audience}");
            Console.WriteLine($"ExpiryInMinutes: {ExpiryInMinutes}");
        }
    }
}
