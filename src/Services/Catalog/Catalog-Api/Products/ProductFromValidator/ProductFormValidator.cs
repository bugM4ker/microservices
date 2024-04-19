using Catalog_Api.Products.CreateProduct;
using FluentValidation;

namespace Catalog_Api.Products.ProductFromValidator
{
    public class ProductFormValidator : AbstractValidator<ProductForm>
    {
        public ProductFormValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Product category is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price must be greater than zero.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description must be lower than 500 charaters");
        }
    }
}
