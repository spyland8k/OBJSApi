using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Products
{
    public class SubCategory
    {
        public int SubcategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        //Referance ParentID
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }


        // one subcategory has many products, 1-N
        public virtual ICollection<Product> Products { get; set; }
    }
}
