using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using System.Collections.Generic;

namespace ArenaGestor.Business
{
    public class RolesService : IRolesService
    {
        private readonly IRolesManagement rolesManagement;
        public RolesService(IRolesManagement rolesManagement)
        {
            this.rolesManagement = rolesManagement;
        }

        public IEnumerable<Role> GetUserRoles()
        {
            return rolesManagement.GetUserRoles();
        }

        public IEnumerable<RoleArtist> GetArtistRoles()
        {
            return rolesManagement.GetArtistRoles();
        }
    }
}
