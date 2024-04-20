using FunFox.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FunFox.Infrastructure.Persistence.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext(configuration);


        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)))
                .AddSingleton<DapperContext>();
        }
    }
}
