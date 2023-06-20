using SchuseOnlineShop.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchuseOnlineShop.Models
{
    public class SubCategory:BaseEntity
    {
        public string Name { get; set; }
        [NotMapped]
        public bool IsSelected { get; set; } = false;
   
        public List<CategorySubCategory> CategorySubCategories { get; set; }
    }
}
