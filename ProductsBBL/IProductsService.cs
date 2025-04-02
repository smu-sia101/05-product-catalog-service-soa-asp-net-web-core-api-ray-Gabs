
namespace ProductsBLL
{
    public interface IProductsService
    {
        void Add(ProductDTO product);
        void Delete(int id);
        IEnumerable<ProductDTO> Get();
        ProductDTO Get(int id);
        void Update(ProductDTO productDto);
    }
}