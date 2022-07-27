


using Place.Common.Exceptions;

namespace Place.Application.Identities.Exceptions
{
    public class PasswordChangedException : AppException
    {
        public PasswordChangedException() :
            base("رمز عبور تغییر کرده است، لطفا مجددا وارد شوید", "password_changed")
        {

        }
    }
}
