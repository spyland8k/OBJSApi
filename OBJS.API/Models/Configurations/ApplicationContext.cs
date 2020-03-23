using Microsoft.EntityFrameworkCore;
using OBJS.API.Models.Advertises;
using OBJS.API.Models.Products;
using OBJS.API.Models.Customers;


namespace OBJS.API.Models
{
    public class ApplicationContext : DbContext 
    {
        //options: "ConnectionString:DefaultConnection"
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<CustomerDetail> CustomerDetails { get; set; }

        public DbSet<Advertise> Advertises { get; set; }
        public DbSet<AdvertiseStatus> AdvertiseStatuses { get; set; }
        public DbSet<AdvertiseState> AdvertiseStates { get; set; }
        public DbSet<AdvertiseFeedback> AdvertiseFeedbacks { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        
        
        
    }
}
