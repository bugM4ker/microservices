namespace Catalog_Api.Products.GetProductById
{
    namespace Catalog_Api.Products.GetProducts
    {
        public class GetProductByIdResult
        {
            public ProductDto Products { get; }

            public GetProductByIdResult(ProductDto products)
            {
                Products = products;
            }
        }

        public class ProductDto
        {
            public string Name { get; set; }
            public List<string> Category { get; set; }
            public string Description { get; set; }
            public string ImageFile { get; set; }
            public decimal Price { get; set; }
        }

    }

}
