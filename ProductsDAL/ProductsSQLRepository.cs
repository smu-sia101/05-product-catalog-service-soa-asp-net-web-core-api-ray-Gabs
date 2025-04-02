using SQLite;

namespace ProductsDAL
{
    public class ProductsSQLRepository : IProductsRepository
    {
        private readonly ISQLiteConnection _sqLiteConnection;
        public ProductsSQLRepository(ISQLiteConnection sqLiteConnection)
        {
            _sqLiteConnection = sqLiteConnection;
            _sqLiteConnection.CreateTable<ProductModel>();
        }
        public void Add(ProductModel product)
        {
            _sqLiteConnection.Insert(product);
        }
        public void Edit(ProductModel product)
        {
            _sqLiteConnection.Insert(product);
        }
        public void Update(ProductModel product)
        {
            _sqLiteConnection.Update(product);
        }
        public void Delete(ProductModel product)
        {
            _sqLiteConnection.Delete(product);
        }
        public ProductModel Get(int id)
        {
            TableQuery<ProductModel> result =
                _sqLiteConnection.Table<ProductModel>().Where(q => q.Id.Equals(id));
            return result.FirstOrDefault();
        }

        public IEnumerable<ProductModel> Get()
        {
            return _sqLiteConnection.Table<ProductModel>();
        }

        public IEnumerable<ProductModel> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}