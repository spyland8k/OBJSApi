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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh-mm-ss}", ApplyFormatInEditMode = true)]
        public DateTime Startdate { get; set; }
        //<td style="text-align:left;">@Html.DisplayFor(modelItem => item.SignDate)</td>

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh-mm-ss}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        // 1-1
        // Advertise state; default value: waiting(1), continue(2), finished(3)
        public int AdvertiseStateId { get; set; } = 1;
        [JsonIgnore]
        public AdvertiseState Advertisestate { get; set; }


        //Navigation Properties, those are using for a nested queries and references
        // 1-1 
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        // N-N / one Advertise has a many AdvertiseDetail
        public virtual ICollection<AdvertiseDetail> AdvertiseDetails { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        //Owner of an advertise
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
