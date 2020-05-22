using Microsoft.EntityFrameworkCore;
using OBJS.API.Models.Advertises;
using OBJS.API.Models.Categories;
using OBJS.API.Models.Customers;
using OBJS.API.Models.Transactions;
using System;

namespace OBJS.API.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerDetail> CustomerDetails { get; set; }
     
        public DbSet<Category> Categories { get; set; }

        public DbSet<Bid> Bids { get; set; }

        public DbSet<Advertise> Advertises { get; set; }
        public DbSet<AdvertiseDetail> AdvertiseDetails { get; set; }
        public DbSet<AdvertiseState> AdvertiseStates { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        //FLUENT API section
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Entity relations manipulation
            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(k => k.AdvertisefeedbackId);

                entity.Property(b => b.Star)
                    .HasDefaultValue(5);

                entity.HasOne(k => k.OwnerCustomer)
                    .WithMany(n => n.FeedbackFrom)
                    .HasForeignKey(c => c.OwnerID)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(k => k.BidderCustomer)
                    .WithMany(c => c.FeedbackTo)
                    .HasForeignKey(c => c.BidderID)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Bid>(entity =>
            {
                entity.HasKey(k => k.BidId);

                entity.HasOne(b => b.Customer)
                    .WithMany(c => c.Bids)
                    .HasForeignKey(c => c.CustomerId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(b => b.Advertise)
                    .WithMany(a => a.Bids)
                    .HasForeignKey(a => a.AdvertiseId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Advertise>(entity =>
            {
                entity.HasKey(k => k.AdvertiseId);
                
                entity.HasOne(b => b.Advertisestate)
                    .WithMany(s => s.Advertises)
                    .HasForeignKey(a => a.AdvertiseStateId)
                    .OnDelete(DeleteBehavior.NoAction);
                
                entity.HasOne(b => b.Category)
                    .WithMany(c => c.Advertises)
                    .HasForeignKey(c => c.CategoryId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(b => b.Customer)
                    .WithMany(c => c.Advertises)
                    .HasForeignKey(c => c.CustomerId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            //Entity seeding data initializer
            modelBuilder.Entity<AdvertiseState>(entity =>
            {
                entity.HasData(
                    new AdvertiseState { AdvertiseStateId = 1, IsStarted = true, IsContinue = false, IsFinished = false },
                    new AdvertiseState { AdvertiseStateId = 2, IsStarted = false, IsContinue = true, IsFinished = false },
                    new AdvertiseState { AdvertiseStateId = 3, IsStarted = false, IsContinue = false, IsFinished = true }
                    );
            });

            modelBuilder.Entity<Category>(entity =>
            {
                //Category test = new Category { CategoryId = 1, Name = "Temizlik", ParentID = null };
                //Hasdata inserting data on given entity if there is no exist on DB
                entity.HasData(
                    new Category { CategoryId = 1, Name = "Temizlik", ParentID = null, },
                    new Category { CategoryId = 2, Name = "Tadilat", ParentID = null },
                    new Category { CategoryId = 3, Name = "Nakliyat", ParentID = null },
                    new Category { CategoryId = 4, Name = "Montaj-Hizmet", ParentID = null },
                    new Category { CategoryId = 5, Name = "Ev Temizliği", ParentID = 1 },
                    new Category { CategoryId = 6, Name = "Koltuk Temizliği", ParentID = 1 },
                    new Category { CategoryId = 7, Name = "Boyama", ParentID = 2 },
                    new Category { CategoryId = 8, Name = "Evden Eve", ParentID = 3 },
                    new Category { CategoryId = 9, Name = "Şehirlerarası", ParentID = 3 },
                    new Category { CategoryId = 10, Name = "Tesisatçı", ParentID = 4 },
                    new Category { CategoryId = 11, Name = "Elektrik Tesisatçısı", ParentID = 10 },
                    new Category { CategoryId = 12, Name = "Su Tesisatçısı", ParentID = 10, }
                    );
            });

        }
    }
}
