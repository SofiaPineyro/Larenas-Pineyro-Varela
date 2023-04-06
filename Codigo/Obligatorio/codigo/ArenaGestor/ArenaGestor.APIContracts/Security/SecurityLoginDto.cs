using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Security
{
    public class SecurityLoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
