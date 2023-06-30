using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchuseOnlineShop.Models.Common;


namespace SchuseOnlineShop.Models
{
    public class Team:BaseEntity
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }
        public string SocialAccount { get; set; }
        [NotMapped, Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }
    }
}
