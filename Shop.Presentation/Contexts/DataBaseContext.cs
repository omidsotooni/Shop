using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shop.Application.Interfaces.Contexts;
using Shop.Common.Roles;
using Shop.Domain.Entities.Carts;
using Shop.Domain.Entities.Finances;
using Shop.Domain.Entities.HomePages;
using Shop.Domain.Entities.Orders;
using Shop.Domain.Entities.Products;
using Shop.Domain.Entities.Users;
using System.Data;

namespace Shop.Presentation.Contexts
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions _options):base(_options)
        {

        }
        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UsersInRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductFeatures> ProductFeatures { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<HomePageImages> HomePageImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public IDbContextTransaction BeginTransaction()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(o => o.Orders)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithMany(o => o.Orders)
                .OnDelete(DeleteBehavior.NoAction);
            //Seed Data
            SeedData(modelBuilder);

            // اعمال ایندکس بر روی فیلد ایمیل واعمال عدم تکراری بودن ایمیل
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            //-- عدم نمایش اطلاعات حذف شده
            ApplyQueryFilter(modelBuilder);
        }

        private void ApplyQueryFilter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(o => !o.IsRemoved);
            modelBuilder.Entity<Role>().HasQueryFilter(o => !o.IsRemoved);
            modelBuilder.Entity<UserInRole>().HasQueryFilter(o => !o.IsRemoved);
            modelBuilder.Entity<Category>().HasQueryFilter(o => !o.IsRemoved);
            modelBuilder.Entity<Product>().HasQueryFilter(o => !o.IsRemoved);
            modelBuilder.Entity<ProductImages>().HasQueryFilter(o => !o.IsRemoved);
            modelBuilder.Entity<ProductFeatures>().HasQueryFilter(o => !o.IsRemoved);
            modelBuilder.Entity<Slider>().HasQueryFilter(o => !o.IsRemoved);
            modelBuilder.Entity<HomePageImages>().HasQueryFilter(o => !o.IsRemoved);
            modelBuilder.Entity<Cart>().HasQueryFilter(o => !o.IsRemoved);
            modelBuilder.Entity<CartItem>().HasQueryFilter(o => !o.IsRemoved);
            modelBuilder.Entity<Payment>().HasQueryFilter(o => !o.IsRemoved);
            modelBuilder.Entity<Order>().HasQueryFilter(o => !o.IsRemoved);
            modelBuilder.Entity<OrderDetail>().HasQueryFilter(o => !o.IsRemoved);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Role>().HasData(new Role { RoleId=1, Name="Admin"});
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = nameof(UserRoles.Admin) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = nameof(UserRoles.Operator) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 3, Name = nameof(UserRoles.Customer) });
        }
    }
}
