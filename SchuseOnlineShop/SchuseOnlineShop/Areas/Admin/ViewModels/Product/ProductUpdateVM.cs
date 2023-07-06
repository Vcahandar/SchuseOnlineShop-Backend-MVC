using SchuseOnlineShop.Models;
using System.ComponentModel.DataAnnotations;

namespace SchuseOnlineShop.Areas.Admin.ViewModels.Product
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        public string SKU { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public decimal DiscountPrice { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public int SaleCount { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public int StockCount { get; set; }

        public List<IFormFile> Photos { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductVideo> ProductVideos { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }



        public ICollection<ProductColor> ColorNames { get; set; }
        public List<int> ColorIds { get; set; } = new();



        public ICollection<ProductSize> SizeNumber { get; set; }
        public List<int> SizeIds { get; set; } = new();
    }
}
