using Blazored.LocalStorage;
using Immerce.Client.Services;
using Refit;

namespace Immerce.Client.Extensions
{
    public static class ServiceExtension
    {
        /// <summary>
        /// Dependencies registration method
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureClientServicesDependencies(this IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICartService, CartService>();

        }

        public static void ConfigureApiClients(this IServiceCollection services)
        {
            services.AddRefitClient<IProductApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7124/api/v1"));
        }
    }
}
