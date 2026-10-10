namespace BlackDigital
{
    /// <summary>
    /// Represents a standard error response returned by an API, containing a message and optional error details.
    /// </summary>
    public class ErrorApiResponse
    {
        /// <summary>
        /// Gets or sets the main error message.
        /// </summary>
        public string Message { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the list of additional error details.
        /// </summary>
        public List<string>? Errors { get; set; }

        /// <summary>
        /// Creates an <see cref="ErrorApiResponse"/> with the specified message and optional errors.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="errors">The optional list of error details.</param>
        /// <returns>A new <see cref="ErrorApiResponse"/> instance.</returns>
        public static ErrorApiResponse Create(string message, List<string>? errors = null)
        {
            return new ErrorApiResponse
            {
                Message = message,
                Errors = errors
            };
        }

        private static IEnumerable<string> ExtractMessages(Exception exception)
        {
            if (exception.InnerException != null)
            {
                foreach (var msg in ExtractMessages(exception.InnerException))
                    yield return msg;
            }

            yield return $"{exception.GetType().Name}: {exception.Message}\n{exception.StackTrace}";
        }

        /// <summary>
        /// Creates an <see cref="ErrorApiResponse"/> from an exception, exposing details only in development or debug environments or for known exception types.
        /// </summary>
        /// <param name="exception">The exception to convert.</param>
        /// <returns>A new <see cref="ErrorApiResponse"/> instance.</returns>
        public static ErrorApiResponse Create(Exception exception)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            bool showDetailError = string.Equals(environment, "Development", StringComparison.OrdinalIgnoreCase);

            var debugMode = Environment.GetEnvironmentVariable("DEBUG__ACTIVE");
            if (!string.IsNullOrEmpty(debugMode) && bool.TryParse(debugMode, out bool isDebug) && isDebug)
                showDetailError = true;

            bool showRealError = showDetailError 
                                    || exception is ArgumentException 
                                    || exception is InvalidOperationException
                                    || exception is UnauthorizedAccessException
                                    || exception is BusinessException;

            return new ErrorApiResponse
            {
                Message = showRealError
                    ? exception.Message
                    : "An unexpected error occurred. Please try again later.",
                Errors = showRealError ? ExtractMessages(exception).ToList() : null
            };
        }
    }
}
