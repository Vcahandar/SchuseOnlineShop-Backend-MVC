using SchuseOnlineShop.Models.Common;

namespace SchuseOnlineShop.Models
{
    public class ProductImage:BaseEntity
    {
        public string ImgName { get; set; }
        public bool IsMain { get; set; }
        public bool IsHover { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
