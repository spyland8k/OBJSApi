using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models
{
    public class Customer
    {
        //We did this because we will be converting this class into a database table and the 
        //column CustomerID will serve as our primary key with the auto-incremented identity.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Email { get; set; }
        
        public string CompanyName { get; set; }
        
        public bool IsActive { get; set; } = true;

        public virtual CustomerType Type { get; set; }
    }
}
