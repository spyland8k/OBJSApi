using OBJS.API.Models.Advertises;
using OBJS.API.Models.Categories;
using OBJS.API.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Customers
{
    public class Customer
    {
        public Customer()
        {
            CustomerDetails = new HashSet<CustomerDetail>();
        }

        //We did this because we will be converting this class into a database table and the 
        //column CustomerId will serve as our primary key with the auto-incremented identity.
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        public bool IsActive { get; set; } = true;

        [Key, Column(Order = 2)]
        [Required]
        public string Username { get; set; }

        [Required]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayFormat(NullDisplayText = "Null name")]
        public string Name { get; set; }

        [DisplayFormat(NullDisplayText = "Null surname")]
        public string Surname { get; set; }

        [Key, Column(Order = 3)]
        [Required]
        public string Email { get; set; }
        
        public string CompanyName { get; set; }

        [Required]
        public bool IsCustomer { get; set; } = true;
        [Required]
        public bool IsSupplier { get; set; } = false;


        public ICollection<CustomerDetail> CustomerDetails { get; set; }

        public ICollection<Advertise> Advertises { get; set; }

        public ICollection<Bid> Bids { get; set; }

        public ICollection<Feedback> FeedbackFrom { get; set; }

        public ICollection<Feedback> FeedbackTo { get; set; }
    }
}
