using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Users
{
    public class UserUpdateUserDto
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public ICollection<UserRoleDto> Roles { get; set; }

    }
}
