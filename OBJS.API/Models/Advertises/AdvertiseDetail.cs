
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Advertises
{
    public class AdvertiseDetail
    {
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdvertiseDetailId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }



        public int AdvertiseId { get; set; }
        public Advertise Advertise { get; set; }
    }
}
