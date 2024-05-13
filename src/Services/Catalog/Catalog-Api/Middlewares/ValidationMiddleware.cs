using Catalog_Api.Attributes;
using FluentValidation;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next; 
    private readonly IServiceProvider _serviceProvider; 

    public ValidationMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint != null)
        {
            var validateWithAttr = endpoint.Metadata.GetMetadata<ValidateWithAttribute>();
            if (validateWithAttr != null)
            {
                var validator = _serviceProvider.GetRequiredService(validateWithAttr.ValidatorType) as IValidator;
                if (validator == null)
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("Internal Server Error: Validator not found.");
                    return;
                }
                var requestType = validator.GetType().BaseType.GetGenericArguments()[0];
                var request = await context.Request.ReadFromJsonAsync(requestType);

                if (request != null)
                {
                    var validationContext = new ValidationContext<object>(request);
                    var validationResult = await validator.ValidateAsync(validationContext);

                    if (!validationResult.IsValid)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsJsonAsync(validationResult.Errors.Select(e => e.ErrorMessage));
                        return;
                    }
                }
            }
        }
        await _next(context);
    }

}
