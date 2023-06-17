using System.ComponentModel.DataAnnotations;

namespace SchuseOnlineShop.Areas.Admin.ViewModels.BrandLogo
{
    public class BrandLogoCreateVM
    {
        [Required]
        public IFormFile Photo { get; set; }
    }
}
