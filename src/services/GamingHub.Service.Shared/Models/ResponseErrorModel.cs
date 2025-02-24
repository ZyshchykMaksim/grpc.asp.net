namespace GamingHub.Service.Shared.Models
{
    /// <summary>
    /// The error response data.
    /// </summary>
    public class ResponseErrorModel
    {
        /// <summary>
        /// Gets or sets the code which unique identifies error reason.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the detailed description of error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the details of error.
        /// </summary>
        public object? Details { get; set; }
    }
}