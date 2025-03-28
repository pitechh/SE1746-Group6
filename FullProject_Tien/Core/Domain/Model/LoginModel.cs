using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Model
{
    public class LoginModel
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
