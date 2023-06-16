using SchuseOnlineShop.Models.Common;

namespace SchuseOnlineShop.Models
{
    public class Category  :BaseEntity
    {
        public string Name { get; set; }
        public ICollection<CategorySubCategory> CategorySubCategories { get; set; }
    }
}
