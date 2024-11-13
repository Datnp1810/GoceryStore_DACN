namespace GoceryStore_DACN.Models.Respones
{
    public class LoginResult
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty; 
        
    }
}
