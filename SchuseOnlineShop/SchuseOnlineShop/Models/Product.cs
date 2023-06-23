using SchuseOnlineShop.Models.Common;

namespace SchuseOnlineShop.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; } = 5;
        public int SaleCount { get; set; }
        public int StockCount { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //public ICollection<CategorySubCategory> CategorySubCategories { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductVideo> ProductVideos { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
    }
}
