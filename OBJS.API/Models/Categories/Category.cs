using Newtonsoft.Json;
using OBJS.API.Models.Advertises;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OBJS.API.Models.Categories
{
    public partial class Category
    {
        public Category()
        {
            this.SubCategory = new HashSet<Category>();
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

        //Navigation properties are virtual
        [ForeignKey("ParentID")]
        public virtual Category ParentCategory { get; set; }
        
        public virtual ICollection<Category> SubCategory { get; set; }

        [JsonIgnore]
        public virtual ICollection<Advertise> Advertises { get; set; }
    }
}
