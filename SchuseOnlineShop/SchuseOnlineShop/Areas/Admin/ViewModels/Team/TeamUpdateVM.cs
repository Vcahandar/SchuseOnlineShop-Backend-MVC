using System.ComponentModel.DataAnnotations;

namespace SchuseOnlineShop.Areas.Admin.ViewModels.Team
{
    public class TeamUpdateVM
    {
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
