using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Models;

namespace RestaurantSystem.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }
        public DbSet<MenuItem>  MenuItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuItem>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
               .Property(o => o.TotalAmount)
               .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Category>()
                .HasData(
                new Category { Id = 1, Name = "مقبلات", Description = "مقبلات وسلطات" },
                new Category { Id = 2, Name = "أطباق رئيسية", Description = "الأطباق الرئيسية" },
                new Category { Id = 3, Name = "حلويات", Description = "حلويات شرقية وغربية" },
                new Category { Id = 4, Name = "مشروبات", Description = "مشروبات ساخنة وباردة" });
            modelBuilder.Entity<Table>()
                .HasData(
                new Table { Id = 1, TableNumber = 1, Capacity = 2, Location = "داخلي", IsAvailable = true },
                new Table { Id = 2, TableNumber = 2, Capacity = 4, Location = "داخلي", IsAvailable = true },
                new Table { Id = 3, TableNumber = 3, Capacity = 4, Location = "داخلي", IsAvailable = true },
                new Table { Id = 4, TableNumber = 4, Capacity = 6, Location = "خارجي", IsAvailable = true },
                new Table { Id = 5, TableNumber = 5, Capacity = 8, Location = "VIP", IsAvailable = true });
        }
    }
}
