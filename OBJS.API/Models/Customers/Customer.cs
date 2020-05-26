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

        [Required]
        public string Username { get; set; }

        [Required]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        // Allow up to 40 uppercase and lowercase 
        // characters. Use standard error.
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
        //ErrorMessage = "Characters are not allowed.")
        public string Name { get; set; }

        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
        public string Surname { get; set; }

        public string Email { get; set; }

        [DisplayFormat(NullDisplayText = "Şirket Değil")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
        public string CompanyName { get; set; }

        [Required]
        public bool IsCustomer { get; set; } = true;
        [Required]
        public bool IsSupplier { get; set; } = false;

        //Navigation Properties, those are using for a nested queries and references
        public virtual ICollection<CustomerDetail> CustomerDetails { get; set; }

        public virtual ICollection<Advertise> Advertises { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }

        public virtual ICollection<Feedback> FeedbackFrom { get; set; }

        public virtual ICollection<Feedback> FeedbackTo { get; set; }
    }
}
