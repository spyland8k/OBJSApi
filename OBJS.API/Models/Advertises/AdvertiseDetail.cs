
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Advertises
{
    public class AdvertiseDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdvertiseDetailID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }



        public int AdvertiseID { get; set; }
        public Advertise Advertise { get; set; }
    }
}
