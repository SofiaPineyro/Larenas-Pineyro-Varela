using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Users
{
    public class UserInsertUserDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<UserRoleDto> Roles { get; set; }

    }
}
