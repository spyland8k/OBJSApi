using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OBJS.API.Models.Customers;
using OBJS.API.Models.Advertises;
using System.Collections.Generic;

namespace OBJS.API.Models.Transactions
{
    public class Bid
    {
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BidId { get; set; }

        [Required]
        public double Price { get; set; }

        // How many [days] in that job will be done
        [Required]
        public int Duration { get; set; }


        //FK; One Advertisement has a many Bid // Cascade delete
        //[ForeignKey("Advertise")]
        public int AdvertiseId { get; set; }
        public virtual Advertise Advertise { get; set; }

        //FK; One bid has a one Supplier // No-Cascade delete
        //[ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }
    }
}
