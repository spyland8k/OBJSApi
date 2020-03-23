using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Advertises
{
    public class AdvertiseState
    {
        public int AdvertiseStateID { get; set; }

        public bool IsStarted { get; set; } = false;

        public bool IsContinue { get; set; } = false;

        public bool IsFinished { get; set; } = false;
    }
}
