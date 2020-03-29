using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using OBJS.API.Models.Customers;
using OBJS.API.Models.Products;
using OBJS.API.Models.Transactions;
using System;

namespace OBJS.API.Models.Advertises
{
    public class Advertise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdvertiseID { get; set; }

        // Active or Passive; default true.
        public bool IsActive { get; set; } = true;

        public DateTime Startdate { get; set; }

        public DateTime EndDate { get; set; }


        // 1-1
        // Advertise state; waiting, continue, finished
        public int AdvertisestateID { get; set; }
        public AdvertiseState Advertisestate { get; set; }

        // 1-N 
        public Feedback Feedback { get; set; }


        // N-N / one Advertise has a many AdvertiseDetail
        public ICollection<AdvertiseDetail> AdvertiseDetails { get; set; }
        public ICollection<Bid> Bids { get; set; }


        [ForeignKey("SubcategoryID")]
        public int CategoryID { get; set; }
        public SubCategory Category { get; set; }

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

    }
}
