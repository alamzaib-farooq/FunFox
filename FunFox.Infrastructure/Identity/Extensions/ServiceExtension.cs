using FunFox.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FunFox.Infrastructure.Identity.Extensions
{
    public static class ServiceExtension
    {
        public static void AddIdentityService(this IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

        }
    }
}
