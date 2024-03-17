namespace Net_Core_WebApp.Models
{
    public class ErrorModel
    {
        public string? ExceptionPath { get; set; }
        public string? ExceptionMessage { get; set; }
        public string? StackTrace { get; set; }
    }
}
