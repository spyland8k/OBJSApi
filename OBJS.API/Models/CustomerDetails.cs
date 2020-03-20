
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models
{
    public class CustomerDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerDetailsId { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string City { get; set; }
    }
}
