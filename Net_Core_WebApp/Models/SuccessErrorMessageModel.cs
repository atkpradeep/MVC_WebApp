namespace Net_Core_WebApp.Models
{
    public class SuccessErrorMessageModel
    {
        public bool IsErrored { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}
