using Microsoft.Extensions.DependencyInjection;
using SQLite;

namespace ProductsDAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProductsRepository(this IServiceCollection services, string dbPath = null)
        {
            if (!string.IsNullOrEmpty(dbPath))
            {
                services.UseSql(dbPath);
            }
            else
            {
                services.UseMongo("mongodb://user:password@localhost:27017", "E-Commerce", "Products");
            }

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
            services.AddSingleton<IProductsRepository>(
                new ProductsSQLMongoRepository(connectionString, dbName, collectionName)
            );
            return services;
        }
    }
}