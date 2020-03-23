using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Customers
{
    public class Customer
    {
        //We did this because we will be converting this class into a database table and the 
        //column CustomerID will serve as our primary key with the auto-incremented identity.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        public bool IsActive { get; set; } = true;

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Email { get; set; }
        
        public string CompanyName { get; set; }
        
        
        //One-to-One Relations -> Customer-CustomerType, Customer-CustomerDetails
        public virtual CustomerType Type { get; set; }
        public virtual CustomerDetail Details { get; set; }
    }
}
