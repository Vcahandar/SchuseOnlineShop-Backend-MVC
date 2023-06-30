using System.ComponentModel.DataAnnotations;

namespace SchuseOnlineShop.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }
}
