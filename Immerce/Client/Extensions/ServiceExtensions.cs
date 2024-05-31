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
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
