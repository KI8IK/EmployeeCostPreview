namespace EmployeeCostPreview.Models
{
    /// <summary>
    /// Service response object returned by all API endpoints.
    /// </summary>
    /// <typeparam name="T">Type of data object the response is carrying</typeparam>
    public class ServiceResponse<T>
    {
        /// <summary>
        /// Response data
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Success status
        /// True if API call was successful
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Surfaceable user-friendly error message.
        /// Only populated when Success is false.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Detailed error information.
        /// Only populated when Success is false.
        /// </summary>
        public string Error { get; set; } = string.Empty;
    }
}
