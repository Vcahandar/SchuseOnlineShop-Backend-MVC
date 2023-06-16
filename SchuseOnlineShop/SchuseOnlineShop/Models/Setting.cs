using SchuseOnlineShop.Models.Common;

namespace SchuseOnlineShop.Models
{
    public class Setting:BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
