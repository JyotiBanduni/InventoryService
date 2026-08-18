namespace InventoryService.Data
{
    using InventoryService.Entities;
    using Microsoft.EntityFrameworkCore;

    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }
        public DbSet<InventoryItems> InventoryItems => Set<InventoryItems>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryItems>(entity =>
            {
                entity.HasKey(e => e.ItemId);
                entity.Property(e => e.ItemName).HasMaxLength(150).IsRequired();
                entity.Property(e => e.Category).HasMaxLength(100);
                entity.HasIndex(e => e.ItemName);
                entity.HasIndex(e => e.IsActive);
            });
        }
    }
   
}
