using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Customers
{
    public class CustomerDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerdetailID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Phone { get; set; }

        //FK Relations
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
    }
}
