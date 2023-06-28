using System.ComponentModel.DataAnnotations;

namespace SchuseOnlineShop.ViewModels.Contact
{
    public class ContactVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
