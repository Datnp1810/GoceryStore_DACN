namespace GoceryStore_DACN.Models.Responses
{
  public class ServiceResponse
  {
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }

    public ServiceResponse()
    {
      Succeeded = true;
      Message = string.Empty;
      Errors = new List<string>();
    }

    public static ServiceResponse Success(string message = null)
    {
      return new ServiceResponse
      {
        Succeeded = true,
        Message = message
      };
    }

    public static ServiceResponse Fail(string message, List<string> errors = null)
    {
      return new ServiceResponse
      {
        Succeeded = false,
        Message = message,
        Errors = errors ?? new List<string>()
      };
    }
  }
}