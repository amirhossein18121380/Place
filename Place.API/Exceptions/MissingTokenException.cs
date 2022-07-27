

using Place.Common.Exceptions;

namespace Place.API.Exceptions
{
    public class MissingTokenException : AppException
    {
        public MissingTokenException() : 
            base("درخواست شما فاقد شناسه احراز هویت می باشد","missing_token")
        {
        }
    }
}
