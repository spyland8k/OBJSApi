using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models
{
    public class CustomerType
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [ForeignKey("Customer")]
        public virtual Customer Customer { get; set; }

        [Required]
        public bool IsCustomer { get; set; } = true;

        [Required]
        public bool IsSupplier { get; set; } = false;
    }
}
