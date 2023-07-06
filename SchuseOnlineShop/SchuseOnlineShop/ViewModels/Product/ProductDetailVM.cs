using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.ViewModels.Product
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public Models.Product Product { get; set; }

        public List<Models.Product> Products { get; set; }

        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public string Video { get; set; }

        public string SKU { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }

        public Category Categories { get; set; }

        public SubCategory SubCategories { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
        public ICollection<CategorySubCategory> CategorySubCategories { get; set; }


        public ICollection<ProductColor> ProductColors { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
        public Dictionary<string, string> SectionBgs { get; set; }
        public ProductCommentVM ProductCommentVM { get; set; }
    }
}
