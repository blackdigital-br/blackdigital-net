using System.Net;

namespace BlackDigital.Rest
{
    public static class HttpHelper
    {
        public static bool IsInformational(this HttpStatusCode httpStatus)
        {
            return (int)httpStatus >= 100 && (int)httpStatus <= 199;
        }

        public static bool IsSuccess(this HttpStatusCode httpStatus)
        {
            return (int)httpStatus >= 200 && (int)httpStatus <= 299;
        }

        public static bool IsRedirection(this HttpStatusCode httpStatus)
        {
            return (int)httpStatus >= 300 && (int)httpStatus <= 399;
        }

        public static bool IsClientError(this HttpStatusCode httpStatus)
        {
            return (int)httpStatus >= 400 && (int)httpStatus <= 499;
        }

        public static bool IsServerError(this HttpStatusCode httpStatus)
        {
            return (int)httpStatus >= 500 && (int)httpStatus <= 599;
        }
    }
}
