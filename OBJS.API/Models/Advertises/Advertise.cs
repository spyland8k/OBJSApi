using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using OBJS.API.Models.Customers;
using OBJS.API.Models.Transactions;
using OBJS.API.Models.Categories;
using System;
using Newtonsoft.Json;

namespace OBJS.API.Models.Advertises
{
    public class Advertise
    {
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdvertiseId { get; set; }

        // Active or Passive; default true.
        // Only Admin can change
        public bool IsActive { get; set; } = true;

        //[DataType(DataType.Date)]
        public DateTime Startdate { get; set; }
        //[DataType(DataType.Date)]
        public DateTime EndDate { get; set; }


        // 1-1
        // Advertise state; waiting, continue, finished
        public int AdvertiseStateId { get; set; }
        [JsonIgnore]
        public AdvertiseState Advertisestate { get; set; }

        // 1-1 
        public Feedback Feedback { get; set; }


        // N-N / one Advertise has a many AdvertiseDetail
        public ICollection<AdvertiseDetail> AdvertiseDetails { get; set; }

        //
        public ICollection<Bid> Bids { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        //Owner of the advertise
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

    }
}
