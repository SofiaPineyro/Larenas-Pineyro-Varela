using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Users
{
    public class UserChangePasswordDto
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
