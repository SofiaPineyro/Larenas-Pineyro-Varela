using System.Collections.Generic;

namespace ArenaGestor.APIContracts.Security
{
    public class SecurityLoggedUser
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public List<SecurityUserRole> Roles { get; set; }
    }
}
