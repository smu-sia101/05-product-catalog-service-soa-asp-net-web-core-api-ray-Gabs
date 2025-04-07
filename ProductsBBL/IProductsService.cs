
namespace ProductsBLL
{
    public interface IProductsService
    {
        void Add(ProductDTO product);
        void Update(ProductDTO product);
        void Delete(string id);
        IEnumerable<ProductDTO> Get();
        ProductDTO Get(string id);
    }
}