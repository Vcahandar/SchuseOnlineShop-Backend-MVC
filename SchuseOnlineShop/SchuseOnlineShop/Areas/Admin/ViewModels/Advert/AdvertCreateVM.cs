using System.ComponentModel.DataAnnotations;

namespace SchuseOnlineShop.Areas.Admin.ViewModels.Advert
{
    public class AdvertCreateVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string ShortDesc { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Title { get; set; }
    }
}
