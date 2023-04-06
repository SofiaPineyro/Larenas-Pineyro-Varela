using ArenaGestor.Domain;
using System.Collections.Generic;

namespace ArenaGestor.BusinessInterface
{
    public interface IRolesService
    {
        IEnumerable<Role> GetUserRoles();
        IEnumerable<RoleArtist> GetArtistRoles();
    }
}
