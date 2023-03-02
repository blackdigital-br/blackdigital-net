
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

        public static void ThrowNotFound() => throw new BusinessException(BusinessExceptionType.NotFound);
        public static void ThrowBadRequest() => throw new BusinessException(BusinessExceptionType.BadRequest);
        public static void ThrowConflict() => throw new BusinessException(BusinessExceptionType.Conflict);
        public static void ThrowGone() => throw new BusinessException(BusinessExceptionType.Gone);
        public static void Throw(string message, int? code = null) => throw new BusinessException(message, code);

        public static object ThrowNotFoundIfNull(object obj) => obj ?? throw new BusinessException(BusinessExceptionType.NotFound);
        public static object ThrowBadRequestIfNull(object obj) => obj ?? throw new BusinessException(BusinessExceptionType.BadRequest);
        public static object ThrowConflictIfNull(object obj) => obj ?? throw new BusinessException(BusinessExceptionType.Conflict);
        public static object ThrowGoneIfNull(object obj) => obj ?? throw new BusinessException(BusinessExceptionType.Gone);
        public static object ThrowIfNull(object obj, string message, int? code = null) => obj ?? throw new BusinessException(message, code);

        public static T ThrowNotFoundIfNull<T>(T? obj) => obj ?? throw new BusinessException(BusinessExceptionType.NotFound);
        public static T ThrowBadRequestIfNull<T>(T? obj) => obj ?? throw new BusinessException(BusinessExceptionType.BadRequest);
        public static T ThrowConflictIfNull<T>(T? obj) => obj ?? throw new BusinessException(BusinessExceptionType.Conflict);
        public static T ThrowGoneIfNull<T>(T? obj) => obj ?? throw new BusinessException(BusinessExceptionType.Gone);
        public static T ThrowIfNull<T>(T? obj, string message, int? code = null) => obj ?? throw new BusinessException(message, code);

        #endregion "Static Methods"
    }
}
