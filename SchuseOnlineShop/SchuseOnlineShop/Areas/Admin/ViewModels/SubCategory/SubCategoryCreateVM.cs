using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchuseOnlineShop.Areas.Admin.ViewModels.SubCategory
{
    public class SubCategoryCreateVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
