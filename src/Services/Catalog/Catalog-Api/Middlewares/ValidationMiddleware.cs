using Catalog_Api.Attributes;
using FluentValidation;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next; // Delegate cho next middleware trong pipeline
    private readonly IServiceProvider _serviceProvider; // Provider để lấy các services

    // Constructor nhận vào next middleware và service provider
    public ValidationMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    // Method được gọi tự động bởi ASP.NET Core khi request đến
    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint != null)
        {
            var validateWithAttr = endpoint.Metadata.GetMetadata<ValidateWithAttribute>();
            if (validateWithAttr != null)
            {
                // Lấy instance của validator một cách an toàn
                var validator = _serviceProvider.GetRequiredService(validateWithAttr.ValidatorType) as IValidator;
                if (validator == null)
                {
                    // Log lỗi hoặc xử lý trường hợp không tìm thấy validator
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("Internal Server Error: Validator not found.");
                    return;
                }

                // Xác định loại đối tượng mà validator hỗ trợ
                var requestType = validator.GetType().BaseType.GetGenericArguments()[0];
                var request = await context.Request.ReadFromJsonAsync(requestType);

                if (request != null)
                {
                    // Tạo ValidationContext từ đối tượng request
                    var validationContext = new ValidationContext<object>(request);

                    // Thực hiện validation
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
