using SchuseOnlineShop.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchuseOnlineShop.Models
{
    public class Shipping:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconImage { get; set; }
    }
}
