


using Place.Common.Exceptions;

namespace Place.Application.Exceptions
{
    public class WrongForgetPasswordRequestException : AppException
    {
        public WrongForgetPasswordRequestException(Guid userId)
            : base("درخواستی مبنی بر فراموشی رمز برای این حساب کاربری وجود ندارد",
                  "wrong_forget_password_request")
        {
        }
        public WrongForgetPasswordRequestException(string mobileNumber)
          : base("درخواستی مبنی بر فراموشی رمز برای این حساب کاربری وجود ندارد",
                "wrong_forget_password_request")
        {
        }
    }
}
