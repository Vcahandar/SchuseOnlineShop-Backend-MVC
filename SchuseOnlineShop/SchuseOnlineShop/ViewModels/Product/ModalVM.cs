using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.ViewModels.Product
{
    public class ModalVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Sku { get; set; }
        public string Category { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }

    }
}
