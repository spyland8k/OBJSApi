﻿using OBJS.API.Models.Customers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Advertises
{
    public class Feedback
    {
        public Feedback()
        {

        }

        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdvertisefeedbackId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        //integer 0-5
        public int Star { get; set; }


        public int AdvertiseId { get; set; }
        public Advertise Advertises { get; set; }

        //Feedback FROM
        public int OwnerID { get; set; }
        public Customer OwnerCustomer { get; set; }

        //Feedback TO
        public int BidderID { get; set; }
        public Customer BidderCustomer { get; set; }
    }
}
