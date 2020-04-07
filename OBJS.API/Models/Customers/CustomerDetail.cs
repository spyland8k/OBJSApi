using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Customers
{
    public class CustomerDetail
    {
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int CustomerdetailId { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public int Phone { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        //Geri döndürülen json'da veriyi gizler
        [JsonIgnore]
        public Customer Customer { get; set; }
    }
}
