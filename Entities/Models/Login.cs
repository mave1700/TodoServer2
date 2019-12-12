using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Login
    {
        [StringLength(30, ErrorMessage = "Username can't be longer than 30 characters")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [StringLength(30, ErrorMessage = "Password can't be longer than 30 characters")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
