using Assignment.Models;
using Microsoft.EntityFrameworkCore;


namespace Assignment.DataAccess
{
    public class BookShopDbContext :DbContext
    {
        public BookShopDbContext(DbContextOptions<BookShopDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> User { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

    }
}
