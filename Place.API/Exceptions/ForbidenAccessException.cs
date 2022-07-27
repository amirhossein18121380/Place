


using Place.Common.Exceptions;

namespace Place.API.Exceptions
{
    public class ForbidenAccessException : AppException
    {
        public ForbidenAccessException() : base("شما مجاز با ارسال این درخواست نیستید.", "forbiden_access")
        {
        }
    }
}
