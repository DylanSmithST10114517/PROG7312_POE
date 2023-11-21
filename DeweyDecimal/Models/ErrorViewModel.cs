namespace DeweyDecimal.Models
{
    // Represents a model for handling and displaying errors in the application
    public class ErrorViewModel
    {
        // Gets or sets the request ID associated with the error
        public string? RequestId { get; set; }

        // Indicates whether the request ID should be shown
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
