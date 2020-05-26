using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OBJS.API.Models.Customers;
using OBJS.API.Models.Advertises;
using Newtonsoft.Json;

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


        //Navigation Properties, those are using for a nested queries and references

        //FK; One Advertisement has a many Bid // Cascade delete
        public int AdvertiseId { get; set; }

        [JsonIgnore]
        public virtual Advertise Advertise { get; set; }

        //FK; One bid has a one Supplier // No-Cascade delete
        public int CustomerId { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }
    }
}
