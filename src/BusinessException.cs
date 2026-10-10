
namespace BlackDigital
{
    /// <summary>
    /// Represents an exception that carries a business error code and provides helper methods for common HTTP-style errors.
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class with a message and optional code.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="code">The optional business error code. Defaults to <see cref="BusinessExceptionType.ExpectationFailed"/>.</param>
        public BusinessException(string message, int? code = null)
            : base(message)
        {
            Code = code ?? (int)BusinessExceptionType.ExpectationFailed;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class from a <see cref="BusinessExceptionType"/>.
        /// </summary>
        /// <param name="type">The type of business exception.</param>
        public BusinessException(BusinessExceptionType type = BusinessExceptionType.ExpectationFailed)
            : base(Enum.GetName(type))
        {
            Code = (int)type;
        }

        /// <summary>
        /// Gets the business error code associated with this exception.
        /// </summary>
        public int Code { get; protected set; }

        #region "Static Methods"

        /// <summary>
        /// Gets a new <see cref="BusinessException"/> representing a Bad Request (400) error.
        /// </summary>
        public static BusinessException BadRequest => new(BusinessExceptionType.BadRequest);

        /// <summary>
        /// Gets a new <see cref="BusinessException"/> representing a Forbidden (403) error.
        /// </summary>
        public static BusinessException Forbidden => new(BusinessExceptionType.Forbidden);

        /// <summary>
        /// Gets a new <see cref="BusinessException"/> representing a Not Found (404) error.
        /// </summary>
        public static BusinessException NotFound => new(BusinessExceptionType.NotFound);

        /// <summary>
        /// Gets a new <see cref="BusinessException"/> representing a Conflict (409) error.
        /// </summary>
        public static BusinessException Conflict => new(BusinessExceptionType.Conflict);

        /// <summary>
        /// Gets a new <see cref="BusinessException"/> representing a Gone (410) error.
        /// </summary>
        public static BusinessException Gone => new(BusinessExceptionType.Gone);

        /// <summary>
        /// Gets a new <see cref="BusinessException"/> representing a Precondition Failed (412) error.
        /// </summary>
        public static BusinessException PreconditionFailed => new(BusinessExceptionType.PreconditionFailed);

        /// <summary>
        /// Gets a new <see cref="BusinessException"/> representing a Range Not Satisfiable (416) error.
        /// </summary>
        public static BusinessException RangeNotSatisfiable => new(BusinessExceptionType.RangeNotSatisfiable);

        /// <summary>
        /// Gets a new <see cref="BusinessException"/> representing an Expectation Failed (417) error.
        /// </summary>
        public static BusinessException ExpectationFailed => new(BusinessExceptionType.ExpectationFailed);

        /// <summary>
        /// Gets a new <see cref="BusinessException"/> representing a Precondition Required (428) error.
        /// </summary>
        public static BusinessException PreconditionRequired => new(BusinessExceptionType.PreconditionRequired);

        /// <summary>
        /// Creates a new <see cref="BusinessException"/> with the specified message and optional code.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="code">The optional business error code.</param>
        /// <returns>A new <see cref="BusinessException"/> instance.</returns>
        public static BusinessException New(string message, int? code = null) => new(message, code);

        /// <summary>
        /// Throws a <see cref="BadRequest"/> exception.
        /// </summary>
        public static void ThrowBadRequest() => throw BadRequest;

        /// <summary>
        /// Throws a <see cref="Forbidden"/> exception.
        /// </summary>
        public static void ThrowForbidden() => throw Forbidden;

        /// <summary>
        /// Throws a <see cref="NotFound"/> exception.
        /// </summary>
        public static void ThrowNotFound() => throw NotFound;

        /// <summary>
        /// Throws a <see cref="Conflict"/> exception.
        /// </summary>
        public static void ThrowConflict() => throw Conflict;

        /// <summary>
        /// Throws a <see cref="Gone"/> exception.
        /// </summary>
        public static void ThrowGone() => throw Gone;

        /// <summary>
        /// Throws a <see cref="PreconditionFailed"/> exception.
        /// </summary>
        public static void ThrowPreconditionFailed() => throw PreconditionFailed;

        /// <summary>
        /// Throws a <see cref="RangeNotSatisfiable"/> exception.
        /// </summary>
        public static void ThrowRangeNotSatisfiable() => throw RangeNotSatisfiable;

        /// <summary>
        /// Throws an <see cref="ExpectationFailed"/> exception.
        /// </summary>
        public static void ThrowExpectationFailed() => throw ExpectationFailed;

        /// <summary>
        /// Throws a <see cref="PreconditionRequired"/> exception.
        /// </summary>
        public static void ThrowPreconditionRequired() => throw PreconditionRequired;

        /// <summary>
        /// Throws a new <see cref="BusinessException"/> with the specified message and optional code.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="code">The optional business error code.</param>
        public static void Throw(string message, int? code = null) => throw New(message, code);

        /// <summary>
        /// Throws a <see cref="BadRequest"/> exception if the specified object is <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>The object if it is not <c>null</c>.</returns>
        public static T ThrowBadRequestIfNull<T>(T? obj) => obj ?? throw BadRequest;

        /// <summary>
        /// Throws a <see cref="Forbidden"/> exception if the specified object is <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>The object if it is not <c>null</c>.</returns>
        public static T ThrowForbiddenIfNull<T>(T? obj) => obj ?? throw Forbidden;

        /// <summary>
        /// Throws a <see cref="NotFound"/> exception if the specified object is <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>The object if it is not <c>null</c>.</returns>
        public static T ThrowNotFoundIfNull<T>(T? obj) => obj ?? throw NotFound;

        /// <summary>
        /// Throws a <see cref="Conflict"/> exception if the specified object is <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>The object if it is not <c>null</c>.</returns>
        public static T ThrowConflictIfNull<T>(T? obj) => obj ?? throw Conflict;

        /// <summary>
        /// Throws a <see cref="Gone"/> exception if the specified object is <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>The object if it is not <c>null</c>.</returns>
        public static T ThrowGoneIfNull<T>(T? obj) => obj ?? throw Gone;

        /// <summary>
        /// Throws a <see cref="PreconditionFailed"/> exception if the specified object is <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>The object if it is not <c>null</c>.</returns>
        public static T ThrowPreconditionFailedIfNull<T>(T? obj) => obj ?? throw PreconditionFailed;

        /// <summary>
        /// Throws a <see cref="RangeNotSatisfiable"/> exception if the specified object is <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>The object if it is not <c>null</c>.</returns>
        public static T ThrowRangeNotSatisfiableIfNull<T>(T? obj) => obj ?? throw RangeNotSatisfiable;

        /// <summary>
        /// Throws an <see cref="ExpectationFailed"/> exception if the specified object is <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>The object if it is not <c>null</c>.</returns>
        public static T ThrowExpectationFailedIfNull<T>(T? obj) => obj ?? throw ExpectationFailed;

        /// <summary>
        /// Throws a <see cref="PreconditionRequired"/> exception if the specified object is <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>The object if it is not <c>null</c>.</returns>
        public static T ThrowPreconditionRequiredIfNull<T>(T? obj) => obj ?? throw PreconditionRequired;

        /// <summary>
        /// Throws a new <see cref="BusinessException"/> with the specified message and code if the object is <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="code">The optional business error code.</param>
        /// <returns>The object if it is not <c>null</c>.</returns>
        public static T ThrowIfNull<T>(T? obj, string message, int? code = null) => obj ?? throw New(message, code);

        #endregion "Static Methods"
    }
}
