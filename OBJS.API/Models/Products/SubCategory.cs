using OBJS.API.Models.Advertises;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Products
{
    public class SubCategory
    {
        [Key]
        public int SubcategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        //Referance ParentID--- Üst Sınıf 1-N
        public int CategoryID { get; set; }
        public Category Category { get; set; }


        // subcategory has many products, N-N
        public Advertise Advertise { get; set; }
    }
}
