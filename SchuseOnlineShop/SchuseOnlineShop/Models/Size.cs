using SchuseOnlineShop.Models.Common;

namespace SchuseOnlineShop.Models
{
    public class Size:BaseEntity
    {
        public int Number { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
    }
}
