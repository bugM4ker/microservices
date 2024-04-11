using Catalog_Api.Models;

namespace Catalog_Api.Products.GetProducts
{
    public class GetProductsResult
    {
        public List<ProductDto> Products { get; }

        public GetProductsResult(List<ProductDto> products)
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
