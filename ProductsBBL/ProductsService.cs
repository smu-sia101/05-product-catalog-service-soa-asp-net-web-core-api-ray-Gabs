using ProductsDAL;

namespace ProductsBLL
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public IEnumerable<ProductDTO> Get()
        {
            var products = _productsRepository.Get();
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Type = p.Type,
                Manufacturer = p.Manufacturer
            }).ToList();
        }

        public ProductDTO Get(string id)
        {
            var product = _productsRepository.Get(id);
            if (product == null) return null;
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Type = product.Type,
                Manufacturer = product.Manufacturer
            };
        }

        public void Add(ProductDTO product)
        {
            var productModel = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Type = product.Type,
                Manufacturer = product.Manufacturer
            };
            _productsRepository.Add(productModel);
        }

        public void Update(ProductDTO product)
        {
            var productModel = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Type = product.Type,
                Manufacturer = product.Manufacturer
            };
            _productsRepository.Update(productModel);
        }

        public void Delete(string id)
        {
            _productsRepository.Delete(id);
        }
    }
}

