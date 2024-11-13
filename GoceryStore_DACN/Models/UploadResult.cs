namespace GoceryStore_DACN.Models
{
    public class UploadResult
    {
        public string PublicId { get; set; }
        public string Url { get; set; }
        public string SecureUrl { get; set; }
        public string Format { get; set; }
        public long Size { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
