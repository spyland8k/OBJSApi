using OBJS.API.Models.Advertises;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Categories
{
    public class Category
    {
        public Category()
        {
            //SubCategories = new HashSet<Category>();
        }

        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required, StringLength(100), Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Category Description")]
        public string Description { get; set; }

        //ParentId is nullable, because top-level categories have no parent.
        public int? ParentID { get; set; }
        
        //-----FK-----
        public Category Parent { get; set; }

        public ICollection<Category> Child { get; set; }

        //Virtual used to manage lazy loading and change tracking
        //public ICollection<Category> SubCategories { get; set; }

        // subcategory has many products, N-N
        public virtual ICollection<Advertise> Advertises { get; set; }

    }
}
