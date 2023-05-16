
namespace BlackDigital
{
    public class BusinessException : Exception
    {
        public BusinessException(string message, int? code = null)
            : base(message)
        {
            Code = code ?? (int)BusinessExceptionType.ExpectationFailed;
        }

        public BusinessException(BusinessExceptionType type = BusinessExceptionType.ExpectationFailed)
            : base(Enum.GetName(type))
        {
            Code = (int)type;
        }

        public int Code { get; protected set; }

        #region "Static Methods"


        public static BusinessException BadRequest => new(BusinessExceptionType.BadRequest);
        public static BusinessException Forbidden => new(BusinessExceptionType.Forbidden);
        public static BusinessException NotFound => new(BusinessExceptionType.NotFound);
        public static BusinessException Conflict => new(BusinessExceptionType.Conflict);
        public static BusinessException Gone => new(BusinessExceptionType.Gone);
        public static BusinessException PreconditionFailed => new(BusinessExceptionType.PreconditionFailed);
        public static BusinessException RangeNotSatisfiable => new(BusinessExceptionType.RangeNotSatisfiable);
        public static BusinessException ExpectationFailed => new(BusinessExceptionType.ExpectationFailed);
        public static BusinessException PreconditionRequired => new(BusinessExceptionType.PreconditionRequired);
        public static BusinessException New(string message, int? code = null) => new(message, code);

        public static void ThrowBadRequest() => throw BadRequest;
        public static void ThrowForbidden() => throw Forbidden;
        public static void ThrowNotFound() => throw NotFound;
        public static void ThrowConflict() => throw Conflict;
        public static void ThrowGone() => throw Gone;
        public static void ThrowPreconditionFailed() => throw PreconditionFailed;
        public static void ThrowRangeNotSatisfiable() => throw RangeNotSatisfiable;
        public static void ThrowExpectationFailed() => throw ExpectationFailed;
        public static void ThrowPreconditionRequired() => throw PreconditionRequired;
        public static void Throw(string message, int? code = null) => throw New(message, code);

        public static T ThrowBadRequestIfNull<T>(T? obj) => obj ?? throw BadRequest;
        public static T ThrowForbiddenIfNull<T>(T? obj) => obj ?? throw Forbidden;
        public static T ThrowNotFoundIfNull<T>(T? obj) => obj ?? throw NotFound;
        public static T ThrowConflictIfNull<T>(T? obj) => obj ?? throw Conflict;
        public static T ThrowGoneIfNull<T>(T? obj) => obj ?? throw Gone;
        public static T ThrowPreconditionFailedIfNull<T>(T? obj) => obj ?? throw PreconditionFailed;
        public static T ThrowRangeNotSatisfiableIfNull<T>(T? obj) => obj ?? throw RangeNotSatisfiable;
        public static T ThrowExpectationFailedIfNull<T>(T? obj) => obj ?? throw ExpectationFailed;
        public static T ThrowPreconditionRequiredIfNull<T>(T? obj) => obj ?? throw PreconditionRequired;
        public static T ThrowIfNull<T>(T? obj, string message, int? code = null) => obj ?? throw New(message, code);

        #endregion "Static Methods"
    }
}
