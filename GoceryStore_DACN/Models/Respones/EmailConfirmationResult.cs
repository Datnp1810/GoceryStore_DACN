namespace GoceryStore_DACN.Models.Respones
{
    public class EmailConfirmationResult
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public static EmailConfirmationResult Success(string message) =>
            new EmailConfirmationResult { Succeeded = true, Message = message };

        public static EmailConfirmationResult Failed(IEnumerable<string> errors) =>
            new EmailConfirmationResult { Succeeded = false, Errors = errors };
    }
}
