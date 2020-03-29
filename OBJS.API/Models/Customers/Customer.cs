using OBJS.API.Models.Advertises;
using OBJS.API.Models.Products;
using OBJS.API.Models.Transactions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Customers
{
    public class Customer
    {
        public Customer()
        {
            //FeedbackFrom = new HashSet<Feedback>();
            //FeedbackTo = new HashSet<Feedback>();
        }

        //We did this because we will be converting this class into a database table and the 
        //column CustomerID will serve as our primary key with the auto-incremented identity.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        public bool IsActive { get; set; } = true;
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Email { get; set; }
        
        public string CompanyName { get; set; }

        [Required]
        public bool IsCustomer { get; set; } = true;
        [Required]
        public bool IsSupplier { get; set; } = false;


        public ICollection<CustomerDetail> CustomerDetails { get; set; }

        public Advertise Advertise { get; set; }

        public ICollection<Bid> Bids { get; set; }

        public virtual ICollection<Feedback> FeedbackFrom { get; set; }

        public virtual ICollection<Feedback> FeedbackTo { get; set; }
    }
}
