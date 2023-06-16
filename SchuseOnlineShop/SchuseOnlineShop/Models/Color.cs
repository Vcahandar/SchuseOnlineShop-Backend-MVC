using SchuseOnlineShop.Models.Common;

namespace SchuseOnlineShop.Models
{
    public class Color :BaseEntity
    {
        public string Name { get; set; }

        public ICollection<ProductColor> ProductColors { get; set; }


    }
}
