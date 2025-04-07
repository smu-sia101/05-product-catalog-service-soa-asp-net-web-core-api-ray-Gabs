using MongoDB.Driver;
using SQLite;

namespace ProductsDAL
{
    public class ProductsSQLRepository : IProductsRepository
    {
        private readonly IMongoCollection<ProductModel> _productsCollection;

        public ProductsSQLRepository(string connectionString, string dbName, string collectionName)
        {
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(dbName);
            _productsCollection = database.GetCollection<ProductModel>(collectionName);
        }

        
        public void Add(ProductModel product)
        {
            _productsCollection.InsertOne(product);
        }

        
        public void Delete(string id)
        {
            _productsCollection.DeleteOne(p => p.Id == id);
        }

        public ProductModel Get(string id)
        {
            return _productsCollection.Find(p => p.Id == id).FirstOrDefault();  
        }

        
        public IEnumerable<ProductModel> Get()
        {
            return _productsCollection.Find(_ => true).ToList();  
        }

  
        public void Update(ProductModel product)
        {
            _productsCollection.ReplaceOne(p => p.Id == product.Id, product);
        }

        
        public IEnumerable<ProductModel> GetProducts()
        {
            return _productsCollection.Find(_ => true).ToList(); 
        }
    }
}