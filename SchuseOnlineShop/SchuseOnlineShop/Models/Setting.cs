using SchuseOnlineShop.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchuseOnlineShop.Models
{
    public class Setting:BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }

        [NotMapped]
        public IFormFile CompanyLogoPhoto { get; set; }

    }
}
