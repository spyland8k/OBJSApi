using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Advertises
{
    public class AdvertiseFeedback
    {
        public int AdvertiseFeedbackID { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }

        // Storing integer value; 0-5
        public int Star { get; set; }
    }
}
