using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Products
{
    public class Category
    {
        public int CategoryID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        //One category has a many subcategories, 1-N
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
