using System.ComponentModel.DataAnnotations;

namespace SchuseOnlineShop.Areas.Admin.ViewModels.Size
{
    public class SizeVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public int Number { get; set; }
    }
}
