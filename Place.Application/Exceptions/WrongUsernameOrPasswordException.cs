


using Place.Common.Exceptions;

namespace Place.Application.Exceptions
{
    public class WrongUsernameOrPasswordException : AppException
    {
        public WrongUsernameOrPasswordException() :
            base("نام کاربری یا رمز عبور اشتبا ه است","wrong_username_pass")
        {
        }
    }
}
