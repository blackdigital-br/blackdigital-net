
namespace BlackDigital
{
    public class BusinessException : Exception
    {
        public BusinessException(string message, int? code = null)
            : base(message)
        {
            Code = code ?? 417;
        }

        public BusinessException(BusinessExceptionType type)
            : base(Enum.GetName(type))
        {
            Code = (int)type;
        }

        public int Code { get; protected set; }

        #region "Static Methods"


        public static BusinessException NotFound => new(BusinessExceptionType.NotFound);
        public static BusinessException BadRequest => new(BusinessExceptionType.BadRequest);
        public static BusinessException Conflict => new(BusinessExceptionType.Conflict);
        public static BusinessException Gone => new(BusinessExceptionType.Gone);
        public static BusinessException New(string message, int? code = null) => new(message, code);

        public static void ThrowNotFound() => throw NotFound;
        public static void ThrowBadRequest() => throw BadRequest;
        public static void ThrowConflict() => throw Conflict;
        public static void ThrowGone() => throw Gone;
        public static void Throw(string message, int? code = null) => throw New(message, code);

        public static object ThrowNotFoundIfNull(object obj) => obj ?? throw NotFound;
        public static object ThrowBadRequestIfNull(object obj) => obj ?? BadRequest;
        public static object ThrowConflictIfNull(object obj) => obj ?? throw Conflict;
        public static object ThrowGoneIfNull(object obj) => obj ?? throw Gone;
        public static object ThrowIfNull(object obj, string message, int? code = null) => obj ?? throw New(message, code);

        public static T ThrowNotFoundIfNull<T>(T? obj) => obj ?? throw NotFound;
        public static T ThrowBadRequestIfNull<T>(T? obj) => obj ?? throw BadRequest;
        public static T ThrowConflictIfNull<T>(T? obj) => obj ?? throw Conflict;
        public static T ThrowGoneIfNull<T>(T? obj) => obj ?? throw Gone;
        public static T ThrowIfNull<T>(T? obj, string message, int? code = null) => obj ?? throw New(message, code);

        #endregion "Static Methods"
    }
}
