

using Place.Common.Exceptions;

namespace Place.Application.Exceptions
{
    public class WrongPasswordException : AppException
    {
        public WrongPasswordException(string mobileNumber)
            : base("رمز  عبور اشتباه است.",
                "wrong_password")
        {
        }
    }
}
