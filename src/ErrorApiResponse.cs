namespace BlackDigital
{
    public class ErrorApiResponse
    {
        public string Message { get; set; } = string.Empty;
        
        public List<string>? Errors { get; set; }

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
