using Microsoft.EntityFrameworkCore;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.Invoices;
using StorageSystem.Infrastructure.Persistence.Contracts;

namespace StorageSystem.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContextBase//IdentityDbContext<ApplicationUser, IdentityRole, string>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void EntityTypeConfiguration(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Gender>();
            modelBuilder.InjectEntities<IEntity>(typeof(Invoice).Assembly);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies();
    }
}
