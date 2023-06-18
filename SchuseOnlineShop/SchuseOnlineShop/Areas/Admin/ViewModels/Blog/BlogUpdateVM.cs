using System.ComponentModel.DataAnnotations;

namespace SchuseOnlineShop.Areas.Admin.ViewModels.Blog
{
    public class BlogUpdateVM
    {
        [Required(ErrorMessage = "Don`t be empty")]

        public string Title { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]

        public string Description { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }

    }
}
