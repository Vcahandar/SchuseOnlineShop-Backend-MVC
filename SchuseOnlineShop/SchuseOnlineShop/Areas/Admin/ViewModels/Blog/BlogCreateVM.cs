using System.ComponentModel.DataAnnotations;

namespace SchuseOnlineShop.Areas.Admin.ViewModels.Blog
{
    public class BlogCreateVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }
    }
}
