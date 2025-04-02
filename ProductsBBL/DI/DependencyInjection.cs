using Microsoft.Extensions.DependencyInjection;
using ProductsDAL.DI;

namespace ProductsBLL.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProductsServices(this IServiceCollection services, string dbPath)
        {
            services.AddSingleton<IProductsService, ProductsService>();
            services.AddProductsRepository(dbPath);
            return services;
        }
    }
}
