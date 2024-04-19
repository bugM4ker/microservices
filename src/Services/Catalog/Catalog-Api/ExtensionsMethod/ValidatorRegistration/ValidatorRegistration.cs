using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Catalog_Api.Products.CreateProduct;
using Catalog_Api.Products.ProductFromValidator;

public static class ValidatorRegistration
{
    public static void AddValidators(this IServiceCollection services)
    {
        // Register Validator here
        // | |
        // V V
        services.AddTransient<IValidator<ProductForm>, ProductFormValidator>();
    }
}