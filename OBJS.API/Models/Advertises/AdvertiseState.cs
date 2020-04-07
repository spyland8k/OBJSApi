using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Advertises
{
    public class AdvertiseState
    {
        [Key]
        public int AdvertiseStateId { get; set; }
        [Required]
        public bool IsStarted { get; set; } = false;
        [Required]
        public bool IsContinue { get; set; } = false;
        [Required]
        public bool IsFinished { get; set; } = false;


        public ICollection<Advertise> Advertises { get; set; }
    }
}
