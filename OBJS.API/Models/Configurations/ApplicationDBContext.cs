using Microsoft.EntityFrameworkCore;
using OBJS.API.Models.Advertises;
using OBJS.API.Models.Products;
using OBJS.API.Models.Customers;
using OBJS.API.Models.Transactions;

namespace OBJS.API.Models
{
    public class ApplicationDBContext : DbContext
    {
        //options: "ConnectionString:DefaultConnection"
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerDetail> CustomerDetails { get; set; }
     
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<Bid> Bids { get; set; }

        public DbSet<Advertise> Advertises { get; set; }
        public DbSet<AdvertiseDetail> AdvertiseDetails { get; set; }
        public DbSet<AdvertiseState> AdvertiseStates { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(k => k.AdvertisefeedbackID);

                entity.HasOne(k => k.OwnerCustomer)
                    .WithMany(n => n.FeedbackFrom)
                    .HasForeignKey(c => c.OwnerID)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(k => k.BidderCustomer)
                    .WithMany(c => c.FeedbackTo)
                    .HasForeignKey(c => c.BidderID)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Bid>(entity =>
            {
                entity.HasKey(k => k.BidID);

                entity.HasOne(b => b.Customer)
                    .WithMany(c => c.Bids)
                    .HasForeignKey(c => c.CustomerID)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Advertise>(entity =>
            {
                entity.HasKey(k => k.AdvertiseID);

                entity.HasOne(b => b.Advertisestate)
                    .WithOne(c => c.Advertise)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(b => b.Category)
                    .WithOne(c => c.Advertise)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(b => b.Customer)
                    .WithOne(c => c.Advertise)
                    .OnDelete(DeleteBehavior.NoAction);

            });

        }

    }
}
