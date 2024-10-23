using Microsoft.EntityFrameworkCore;
using StorageSystem.Order.Domain.Commons.Interfaces;
using StorageSystem.Order.Domain.Entities.Invoices;
using StorageSystem.Order.Persistence.Contracts;

namespace StorageSystem.Order.Persistence.Data
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
