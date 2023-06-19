using System.ComponentModel.DataAnnotations;

namespace SchuseOnlineShop.Areas.Admin.ViewModels.HomeCategory
{
    public class HomeCategoryCreateVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }

    }
}
