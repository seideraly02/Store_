using Microsoft.EntityFrameworkCore;

namespace Store.Models
{
    public class StoreContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options) : 
            base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role {Id = 1, Name = "admin"});
            modelBuilder.Entity<Role>().HasData(new Role {Id = 2, Name = "user"});
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1, 
                Email = "admin@admin.admin",
                UserName = "admin",
                Password = "admin",
                RoleId = 1
            });
        }
    }
}