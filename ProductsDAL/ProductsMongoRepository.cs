using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace ProductsDAL
{
    public class ProductsSQLMongoRepository : IProductsRepository
    {
        private readonly IMongoCollection<ProductModel> _productsCollection;

        public ProductsSQLMongoRepository(string connectionString, string dbName, string collectionName)
        {
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(dbName);
            _productsCollection = database.GetCollection<ProductModel>(collectionName);
        }

        public void Add(ProductModel product)
        {
            _productsCollection.InsertOne(product);
        }

        public void Delete(ProductModel product)
        {
            _productsCollection.DeleteOne(p => p.Id == product.Id);
        }

        public IEnumerable<ProductModel> Get()
        {
            return _productsCollection.Find(_ => true).ToList();
        }

        public ProductModel Get(int id)
        {
            return _productsCollection.Find(p => p.Id == id).FirstOrDefault();
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
