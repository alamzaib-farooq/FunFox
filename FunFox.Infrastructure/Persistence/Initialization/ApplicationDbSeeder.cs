using FunFox.Infrastructure.Identity;
using FunFox.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace FunFox.Infrastructure.Persistence.Initialization
{
    public static class ApplicationDbSeeder
    {
        public static async Task SeedRolesAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
            string[] roles = new string[] { "Administrator", "User" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(_dbContext);

                if (!_dbContext.Roles.Any(r => r.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole()
                    {
                        Name = role,
                        NormalizedName = role.ToUpperInvariant()
                    });
                }
            }
        }

        public static async Task SeedAdminUserAsync(this IServiceProvider serviceProvider)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var _dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var _userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var _roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var admin = new ApplicationUser()
                {
                    Email = "portaladmin@gmail.com",
                    NormalizedEmail = "PORTALADMIN@GMAIL.COM",
                    UserName = "portaladmin@gmail.com",
                    NormalizedUserName = "portaladmin@gmail.com",
                    PhoneNumber = "+300000000",
                    EmailConfirmed = true,
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };


                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(admin, "12345678");
                admin.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(_dbContext);
                var result = userStore.CreateAsync(admin);

                var allRoles = await _roleManager.Roles.ToListAsync();

                await AssignRoles(_userManager!, admin.Email, allRoles.Where(x => x.Name == "Administrator").Select(x => x.Name).ToList());

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

            }

        }

        public static async Task<IdentityResult> AssignRoles(UserManager<ApplicationUser> userManager, string email, List<string?> roles)
        {
            ApplicationUser user = (await userManager.FindByEmailAsync(email))!;
            var result = await userManager.AddToRolesAsync(user, roles);

            return result;
        }
    }
}
