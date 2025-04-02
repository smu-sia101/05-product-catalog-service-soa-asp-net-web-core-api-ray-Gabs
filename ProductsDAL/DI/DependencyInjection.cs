using Microsoft.Extensions.DependencyInjection;
using SQLite;

namespace ProductsDAL.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProductsRepository(this IServiceCollection services, string dbPath)
        {
            services.AddSingleton<IProductsRepository, ProductsRepository>();
            services.AddSingleton<ISQLiteConnection>(new SQLiteConnection(dbPath));
            return services;
        }
    }
}
