using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SQLite;

namespace ProductsDAL.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProductsRepository(this IServiceCollection services, string connectionString, string dbName, string collectionName)
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");  // Use this if no authentication is needed
            var database = mongoClient.GetDatabase(dbName);
            var collection = database.GetCollection<ProductModel>(collectionName);
            services.AddSingleton<IProductsRepository>(new ProductsSQLMongoRepository(collection));
            return services;
        }

        // Alternatively, you can define a method to use SQL (if needed)
        public static IServiceCollection UseSql(this IServiceCollection services, string dbPath)
        {
            services.AddSingleton<ISQLiteConnection>(new SQLiteConnection(dbPath));
            services.AddSingleton<IProductsRepository, ProductsSQLRepository>();
            return services;
        }
    }
}
