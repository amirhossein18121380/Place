
namespace Place.Common.Exceptions
{
    public class AppException : Exception
    {
        public virtual string Code { get; }

        protected AppException(string message, string code = "") : base(message)
        {
            Code = code;
        }
    }
}
