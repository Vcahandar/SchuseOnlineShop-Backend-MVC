using SchuseOnlineShop.Models.Common;

namespace SchuseOnlineShop.Models
{
    public class Blog:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
