using SchuseOnlineShop.Models.Common;

namespace SchuseOnlineShop.Models
{
    public class ProductComment  : BaseEntity
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
