namespace Catalog_Api.Products.CreateProduct
{
    public class CreateProductResult
    {
        public CreateProductResult(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
