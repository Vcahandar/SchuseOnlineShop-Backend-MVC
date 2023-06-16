using SchuseOnlineShop.Models.Common;

namespace SchuseOnlineShop.Models
{
    public class Slider:BaseEntity
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Heading { get; set; }
        public string Desc { get; set; }
    }
}
