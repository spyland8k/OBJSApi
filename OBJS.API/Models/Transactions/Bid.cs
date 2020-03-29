using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OBJS.API.Models.Customers;
using OBJS.API.Models.Advertises;
using System.Collections.Generic;

namespace OBJS.API.Models.Transactions
{
    public class Bid
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BidID { get; set; }

        [Required]
        public double Price { get; set; }

        // How many days in that job will be done
        [Required]
        public int Duration { get; set; }


        //FK; One Advertisement has a many Bid // Cascade delete
        //[ForeignKey("Advertise")]
        public int AdvertiseID { get; set; }
        public Advertise Advertise { get; set; }

        //FK; One bid has a one Supplier // No-Cascade delete
        //[ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
    }
}
