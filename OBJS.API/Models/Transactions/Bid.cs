using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OBJS.API.Models.Customers;
using OBJS.API.Models.Advertises;

namespace OBJS.API.Models.Transactions
{
    public class Bid
    {
        public int BidID { get; set; }

        public double Price { get; set; }

        // How many days in that job will be done
        public int Duration { get; set; }


        //FK; One Advertisement has a many Bid
        public int AdvertiseID { get; set; }
        public Advertise Advertises { get; set; }

        //FK; One bid has a one Supplier 
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
    }
}
