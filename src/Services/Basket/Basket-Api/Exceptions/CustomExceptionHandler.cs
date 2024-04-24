namespace Basket_Api.Exceptions
{
    public class CustomExceptionHandler : Exception
    {
        public int statusCode {  get;} 
        public CustomExceptionHandler(string? message, int _statusCode) : base(message)
        {
            statusCode = _statusCode;
        }
    }
}
