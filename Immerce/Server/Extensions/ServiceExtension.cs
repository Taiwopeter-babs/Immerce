using Immerce.Server.Data;
using Immerce.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace Immerce.Server.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }

        /// <summary>
        /// Dependencies registration method
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServicesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
