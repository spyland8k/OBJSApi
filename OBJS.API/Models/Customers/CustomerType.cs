using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Customers
{
    public class CustomerType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomertypeID { get; set; }
        public bool IsCustomer { get; set; } = true;
        public bool IsSupplier { get; set; } = false;

        //FK Relations
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
    }
}
