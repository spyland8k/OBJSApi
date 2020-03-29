using OBJS.API.Models.Customers;
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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdvertisefeedbackID { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        //integer 0-5
        public int Star { get; set; }


        public int AdvertiseID { get; set; }
        public Advertise Advertises { get; set; }

        //Feedback FROM
        public int OwnerID { get; set; }
        public Customer OwnerCustomer { get; set; }

        //Feedback TO
        public int BidderID { get; set; }
        public virtual Customer BidderCustomer { get; set; }
    }
}
