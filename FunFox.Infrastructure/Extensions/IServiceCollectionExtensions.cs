using FluentValidation;
using FluentValidation.AspNetCore;
using FunFox.Application.Contracts;
using FunFox.Application.Contracts.Repositories;
using FunFox.Application.Dtos;
using FunFox.Application.Services;
using FunFox.Infrastructure.Identity.Extensions;
using FunFox.Infrastructure.Persistence.Extensions;
using FunFox.Infrastructure.Persistence.Initialization;
using FunFox.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace FunFox.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddPersistence(config);
            services.AddIdentityService();
            services.AddRepositories();
            services.ConfigureFluentValidation();
        }
        public static async Task SeedUsersAndRoles(this IServiceProvider provider)
        {
            await provider.SeedRolesAsync();
            await provider.SeedAdminUserAsync();
        }

        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddHttpContextAccessor()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<CourseDto>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddScoped<ICourseRepository, CourseRepository>()
                .AddScoped<IEnrollmentRepository, EnrollmentRepository>()
                .AddScoped<ICourseService, CourseService>();
        }


        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder) =>
            builder
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization();

        public static IApplicationBuilder UseExceptionHandlers(this IApplicationBuilder builder, IHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                builder.UseDeveloperExceptionPage();
            }
            else
            {
                builder.UseExceptionHandler("/Home/Error");
                builder.UseHsts();
            }

            return builder;
        }

        public static ConfigureHostBuilder UseSerilogLogger(this WebApplicationBuilder builder, ConfigureHostBuilder host)
        {
            host.UseSerilog((_, config) =>
            {
                config.WriteTo.Console()
                    .ReadFrom.Configuration(builder.Configuration);
            });
            return host;
        }

        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            builder.MapRazorPages();
            return builder;
        }
    }
}
