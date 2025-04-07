
namespace ProductsDAL
{
    public interface IProductsRepository
    {
        void Add(ProductModel product);
        void Delete(string id);
        IEnumerable<ProductModel> Get();
        ProductModel Get(string id);
        void Update(ProductModel product);
    }
}