
namespace ProductsDAL
{
    public interface IProductsRepository
    {
        void Add(ProductModel product);
        void Delete(ProductModel product);
        IEnumerable<ProductModel> Get();
        ProductModel Get(int id);
        void Update(ProductModel product);
        IEnumerable<ProductModel> GetProducts();
    }
}