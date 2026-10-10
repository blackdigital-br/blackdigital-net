
namespace BlackDigital
{
    /// <summary>
    /// Defines the types of business exceptions, mapped to HTTP status codes.
    /// </summary>
    public enum BusinessExceptionType
    {
        /// <summary>
        /// The server cannot process the request due to a client error (HTTP 400).
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// The server refuses to authorize the request (HTTP 403).
        /// </summary>
        Forbidden = 403,

        /// <summary>
        /// The requested resource could not be found (HTTP 404).
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// A conflict occurred in the request (HTTP 409).
        /// </summary>
        Conflict = 409,

        /// <summary>
        /// The requested resource is no longer available (HTTP 410).
        /// </summary>
        Gone = 410,

        /// <summary>
        /// A precondition specified in the request was not met (HTTP 412).
        /// </summary>
        PreconditionFailed = 412,

        /// <summary>
        /// The requested range is not satisfiable (HTTP 416).
        /// </summary>
        RangeNotSatisfiable = 416,

        /// <summary>
        /// The server cannot meet the requirements of the Expect header (HTTP 417).
        /// </summary>
        ExpectationFailed = 417,

        /// <summary>
        /// The origin server requires the request to be conditional (HTTP 428).
        /// </summary>
        PreconditionRequired = 428,
    }
}
