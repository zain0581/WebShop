using IMSWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace IMSWeb.Dal
{
    public class IMSContext : DbContext
    {
        public IMSContext(DbContextOptions options) : base(options)
        {


        }


       
        public DbSet<Category> Categories { get; set; }

        public DbSet<InventoryItems> InventoryItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Customer> Customers { get; set; }
     
        public DbSet<Supplier> Suppliers { get; set; }








    }
}
