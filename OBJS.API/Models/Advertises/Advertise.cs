using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OBJS.API.Models.Customers;
using OBJS.API.Models.Products;

namespace OBJS.API.Models.Advertises
{
    public class Advertise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdvertiseID { get; set; }

        // Active or Passive; default true.
        public int AdvertiseStatusID { get; set; }
        public virtual AdvertiseStatus AdvertiseStatus { get; set; }

        // Started, Continue and Finished; default false;
        public int AdvertiseStateID { get; set; }
        public virtual AdvertiseState AdvertiseState { get; set; }

        //Active after job done
        public int AdvertiseFeedbackID { get; set; }
        public virtual AdvertiseFeedback AdvertiseFeedback { get; set; }


        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}
