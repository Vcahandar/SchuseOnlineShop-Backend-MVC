using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.ViewModels.Product
{
    public class ModalVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public string BrandName { get; set; }
        public string Sku { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }

    }
}
