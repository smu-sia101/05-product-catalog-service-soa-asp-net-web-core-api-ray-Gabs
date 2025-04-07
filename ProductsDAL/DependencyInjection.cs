using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SQLite;


namespace ProductsDAL
{
    public static class DependencyInjection
    {
       
        public static IServiceCollection AddProductsRepository(this IServiceCollection services, string connectionString, string dbName, string collectionName)
        {
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(dbName);
            var collection = database.GetCollection<ProductModel>(collectionName);
            services.AddSingleton<IProductsRepository>(new ProductsSQLMongoRepository(collection));
            return services;
        }

        public static IServiceCollection UseSql(this IServiceCollection services, string dbPath)
        {
            services.AddSingleton<ISQLiteConnection>(new SQLiteConnection(dbPath));
            services.AddSingleton<IProductsRepository, ProductsSQLRepository>(); 
            return services;
        }

        public static IServiceCollection UseMongo(this IServiceCollection services, string connectionString, string dbName, string collectionName)
        {
            // Register ProductsSQLMongoRepository with MongoDB connection details
            services.AddSingleton<IProductsRepository>(provider =>
            {
                var mongoClient = new MongoClient(connectionString);  // Ensure you're passing the correct MongoDB connection string
                var database = mongoClient.GetDatabase(dbName);        // Get the database
                var collection = database.GetCollection<ProductModel>(collectionName); // Get the collection

                return new ProductsSQLMongoRepository(collection);
            });

            return services;
        }
    }
}