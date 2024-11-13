namespace GoceryStore_DACN.Models.Respones
{
    public class ServiceResult
    {
        public bool Succeeded { get; protected set; }      
        public string Message { get; protected set; }      
        public IEnumerable<string> Errors { get; protected set; }  
        public object Data { get; protected set; }
        protected ServiceResult()
        {
            Succeeded = false;
            Errors = new List<string>();
        }

        public static ServiceResult Success(string message = null, object data = null)
        {
            return new ServiceResult
            {
                Succeeded = true,
                Message = message,
                Data = data
            };
        }

        public static ServiceResult Error(string error)
        {
            return new ServiceResult
            {
                Succeeded = false,
                Errors = new[] { error }
            };
        }

        public static ServiceResult Error(IEnumerable<string> errors)
        {
            return new ServiceResult
            {
                Succeeded = false,
                Errors = errors
            };
        }
    }
}
