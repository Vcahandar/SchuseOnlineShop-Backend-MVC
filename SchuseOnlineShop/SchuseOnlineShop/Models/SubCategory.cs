using SchuseOnlineShop.Models.Common;

namespace SchuseOnlineShop.Models
{
    public class SubCategory:BaseEntity
    {
        public string Name { get; set; }
        //public bool IsSelected { get; set; }
        public List<CategorySubCategory> CategorySubCategories { get; set; }
    }
}
