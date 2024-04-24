using Basket_Api.Exceptions;

namespace Basket_Api.Middleware
{
    public class ExceptionHandlerMiddlerware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddlerware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = StatusCodes.Status500InternalServerError; // Default to 500 if unexpected
            var message = "An unexpected error has occurred.";

            if (exception is CustomExceptionHandler customEx)
            {
                statusCode = customEx.statusCode; // Use custom status code
                message = customEx.Message; // Use custom message
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(new
            {
                error = message,
                statusCode = statusCode
            }.ToString());
        }
    }
}
