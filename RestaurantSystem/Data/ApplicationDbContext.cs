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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuItem>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Category>()
                .HasData(
                new Category { Id = 1, Name = "مقبلات", Description = "مقبلات وسلطات" },
                new Category { Id = 2, Name = "أطباق رئيسية", Description = "الأطباق الرئيسية" },
                new Category { Id = 3, Name = "حلويات", Description = "حلويات شرقية وغربية" },
                new Category { Id = 4, Name = "مشروبات", Description = "مشروبات ساخنة وباردة" });
        }
    }
}
