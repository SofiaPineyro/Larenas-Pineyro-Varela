using System.Collections.Generic;

namespace ArenaGestor.APIContracts.Users
{
    public class UserResultUserDto
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }
        public List<UserRoleDto> Roles { get; set; }
    }
}
