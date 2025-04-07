using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace ProductsDAL
{
    public class ProductsSQLMongoRepository : IProductsRepository
    {
        private readonly IMongoCollection<ProductModel> _productsCollection;

        public ProductsSQLMongoRepository(IMongoCollection<ProductModel> productsCollection)
        {
            _productsCollection = productsCollection;
        }

        public void Add(ProductModel product)
        {
            _productsCollection.InsertOne(product);
        }

        public void Delete(string id)
        {
            _productsCollection.DeleteOne(p => p.Id == id); 
        }

        public IEnumerable<ProductModel> Get()
        {
            try
            {
                return _productsCollection.Find(_ => true).ToList();  
            }
            catch (MongoAuthenticationException authEx)
            {
                
                Console.WriteLine("Authentication failed: " + authEx.Message);
                return Enumerable.Empty<ProductModel>();  
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("An error occurred: " + ex.Message);
                return Enumerable.Empty<ProductModel>();  
            }
        }

        public ProductModel Get(string id)
        {
            return _productsCollection.Find(p => p.Id == id).FirstOrDefault(); 
        }

        public void Update(ProductModel product)
        {
            _productsCollection.ReplaceOne(p => p.Id == product.Id, product); 
        }
    }
}
