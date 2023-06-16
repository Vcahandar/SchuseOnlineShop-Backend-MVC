using SchuseOnlineShop.Models.Common;

namespace SchuseOnlineShop.Models
{
    public class ProductVideo : BaseEntity
    {
        public string VideoName { get; set; }
        public string VideoPoster { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
