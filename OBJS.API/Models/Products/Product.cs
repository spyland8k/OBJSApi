using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Products
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        //FK relation
        public int SubcategoryID { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
