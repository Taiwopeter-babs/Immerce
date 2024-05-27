using Immerce.Server.Data;
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
    }
}
