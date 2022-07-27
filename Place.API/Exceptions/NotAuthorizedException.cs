
using Place.Common.Exceptions;

namespace Place.API.Exceptions
{
    public class NotAuthorizedException : AppException
    {
        public NotAuthorizedException() : base("لطفا مجددا وارد سیستم شوید.", "not_authorized")
        {
        }
    }
}
