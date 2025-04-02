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

        public void Add(ProductDTO product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            ProductModel model = MapDtoToModel(product);

            _productsRepository.Add(model);
        }

        public void Update(ProductDTO product)
        {
            var model = MapDtoToModel(product);
            _productsRepository.Update(model);
        }
        public void Delete(int id)
        {
            var product = _productsRepository.Get(id);
            if (product != null)
            {
                _productsRepository.Delete(product);
            }
        }
        public ProductDTO Get(int id)
        {
            ProductModel model = _productsRepository.Get(id);

            if (model == null)
            {
                throw new InvalidOperationException("Product not found.");
            }
            ProductDTO dto = MapModelToDTO(model);

            return dto;
        }

        public IEnumerable<ProductDTO> Get()
        {
            IList<ProductDTO> dTOs = new List<ProductDTO>();

            IEnumerable<ProductModel> models = _productsRepository.Get();
            foreach (ProductModel model in models)
            {
                ProductDTO dto = MapModelToDTO(model);
                dTOs.Add(dto);
            }
            return dTOs;
        }

        private static ProductDTO MapModelToDTO(ProductModel model)
        {
            return new ProductDTO()
            {
                Id = model.Id,
                Manufacturer = model.Manufacturer,
                Type = model.Type,
                Name = model.Name
            };
        }

        private static ProductModel MapDtoToModel(ProductDTO product)
        {
            return new ProductModel()
            {
                Id = product.Id,
                Manufacturer = product.Manufacturer,
                Type = product.Type,
                Name = product.Name
            };
        }
    }
}
