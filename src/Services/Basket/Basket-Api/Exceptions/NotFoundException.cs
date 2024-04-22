namespace Basket_Api.Exceptions
{
    public class NotFoundException : CustomExceptionHandler
    {
        public NotFoundException(string userName) : base($"UserName {userName}'s Basket is Not Found ", 400)
        {
        }
    }
}
