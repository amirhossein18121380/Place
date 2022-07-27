


using Place.Common.Exceptions;

namespace Place.Application.Exceptions
{
    public class WrongOldPasswordException : AppException
    {
        public WrongOldPasswordException(Guid userId)
            : base("رمز عبور قبلی اشتباه است.",
                  "wrong_old_password")
        {
        }
        public WrongOldPasswordException(string mobileNumber)
            : base("رمز عبور قبلی اشتباه است.",
                "wrong_old_password")
        {
        }
    }
}
