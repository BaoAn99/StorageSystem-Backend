using Microsoft.EntityFrameworkCore;

namespace StorageSystem.Infrastructure.Persistence.Contracts
{
    public abstract class DbContextBase : DbContext
    {
        public DbContextBase(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");
            EntityTypeConfiguration(modelBuilder);
            ApplyConfigurationsFromAssembly(modelBuilder);
            modelBuilder.AddDefaultEntitySetting();
        }

        protected virtual void ApplyConfigurationsFromAssembly(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        protected abstract void EntityTypeConfiguration(ModelBuilder modelBuilder);
    }
}
