using System.ComponentModel.DataAnnotations;

namespace SchuseOnlineShop.Areas.Admin.ViewModels.Category
{
    public class CategoryUpdateVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
    }
}
