using Microsoft.Extensions.DependencyInjection;
using ProductsDAL.DI;

namespace ProductsBLL.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProductsServices(this IServiceCollection services, string connectionString, string dbName, string collectionName)
        {
           
            services.AddSingleton<IProductsService, ProductsService>();

         
            services.AddProductsRepository(connectionString, dbName, collectionName); 

            return services;
        }
    }
}
