using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Advertises
{
    public class AdvertiseStatus
    {
        public int AdvertiseStatusID { get; set; }

        // Active or Passive; default true.
        public bool IsActive { get; set; } = true;
    }
}
