using FunFox.Domain.Common.Interfaces;
using FunFox.Domain.Entities;
using FunFox.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FunFox.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //Get current user Id
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                await base.SaveChangesAsync(cancellationToken);
                return 1;
            }

            HandleAuditingBeforeSaveChanges(new Guid(userId));

            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        private void HandleAuditingBeforeSaveChanges(Guid userId)
        {

            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = userId;
                        entry.Entity.CreatedOn = entry.Entity.CreatedOn;
                        entry.Entity.CreatedDate = DateTime.Now.Date;
                        entry.Entity.CreatedTime = DateTime.Now.Ticks;
                        entry.Entity.DateModified = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.DateModified = DateTime.Now;
                        break;
                }
            }
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Enrollment>().HasKey(sc => new { sc.UserId, sc.CourseId });

        }
    }
}
