namespace GamingHub.Service.Shared.Models
{
    /// <summary>
    /// The response model with data.
    /// </summary>
    /// <typeparam name="T">The type of response data. </typeparam>
    public class ResponseDataModel<T>
    {
        /// <summary>
        /// Gets or sets the current server UTC time.
        /// </summary>
        public DateTime ServerTime { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        public ResponseErrorModel? Error { get; set; }

        /// <summary>
        /// Gets or sets the response data.
        /// </summary>
        public T? Data { get; set; }
    }
}