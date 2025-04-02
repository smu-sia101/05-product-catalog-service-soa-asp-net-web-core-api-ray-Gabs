using ProductsBLL;
using System.Reflection;
using Model = MonolithApp.Demo.Models;

namespace MonolithApp.Demo.Extensions
{
    public static class ProductsMapper
    {
        public static ProductDTO MapWebModelToDto(this Model.Product product)
        {
            return new ProductDTO
            {
                Name = product.Name,
                Type = product.Type,
                Manufacturer = product.Manufacturer,
            };
        }

        public static Model.Product MapDtoToWebModel(this ProductDTO product)
        {
            return new Model.Product
            {
                Id = product.Id,
                Name = product.Name,
                Type = product.Type,
                Manufacturer = product.Manufacturer,
            };
        }

        public static List<Model.Product> MapProductsDALToProductsModel(this IEnumerable<ProductDTO> products)
        {
            List<Model.Product> mapped = new List<Model.Product>();
            foreach (ProductDTO product in products)
            {
                Model.Product _product = MapDtoToWebModel(product);
                mapped.Add(_product);
            }

            return mapped;
        }
    }
}
