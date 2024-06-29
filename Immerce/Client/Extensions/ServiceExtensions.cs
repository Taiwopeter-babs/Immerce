using Blazored.LocalStorage;
using Immerce.Client.Services;

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
    }
}
