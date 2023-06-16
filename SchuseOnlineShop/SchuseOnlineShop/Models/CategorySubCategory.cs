using SchuseOnlineShop.Models.Common;

namespace SchuseOnlineShop.Models
{
    public class CategorySubCategory :BaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
