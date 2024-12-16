using Microsoft.EntityFrameworkCore;

namespace Entity_Study_3.Entities
{
    public class ShopContext : DbContext
    {
        // Đặt tên biến cho chuỗi kết nối
        private const string ConnectionString =
            @"Server=DESKTOP-BAIQDRH\SQLEXPRESS; Database=ShopData; User ID=sa; Password=newpassword; TrustServerCertificate=True;";

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}