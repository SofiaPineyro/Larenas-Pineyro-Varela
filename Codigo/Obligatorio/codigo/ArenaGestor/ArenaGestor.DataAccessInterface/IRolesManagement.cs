using ArenaGestor.Domain;
using System.Collections.Generic;

namespace ArenaGestor.DataAccessInterface
{
    public interface IRolesManagement
    {
        IEnumerable<Role> GetUserRoles();
        IEnumerable<RoleArtist> GetArtistRoles();
    }
}
