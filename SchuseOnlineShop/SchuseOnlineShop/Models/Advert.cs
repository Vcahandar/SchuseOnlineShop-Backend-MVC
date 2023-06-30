using SchuseOnlineShop.Models.Common;

namespace SchuseOnlineShop.Models
{
    public class Advert:BaseEntity
    {
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string Image { get; set; }
    }
}
