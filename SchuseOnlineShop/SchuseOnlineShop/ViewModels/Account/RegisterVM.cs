using System.ComponentModel.DataAnnotations;

namespace SchuseOnlineShop.ViewModels.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        [DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
