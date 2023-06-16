using SchuseOnlineShop.Models.Common;

namespace SchuseOnlineShop.Models
{
    public class CartProduct : BaseEntity
    {
        public int Count { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }

    }
}
