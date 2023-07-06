using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.Areas.Admin.ViewModels.Product
{
    public class ProductDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public int SaleCount { get; set; }
        public int Rating { get; set; }
        public int StockCount { get; set; }
        public string BrandName { get; set; }

        public ICollection<ProductColor> ColorNames { get; set; }
        public string CategoryNames { get; set; }
        public string SubCategoryNames { get; set; }
        public ICollection<ProductSize> SizeNames { get; set; }
        public ICollection<ProductImage> Images { get; set; }
    }
}
