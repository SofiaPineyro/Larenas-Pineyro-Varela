using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ArenaGestor.DataAccess.Managements
{
    public class RolesManagement : IRolesManagement
    {
        private readonly DbSet<Role> userRoles;
        private readonly DbSet<RoleArtist> artistRoles;

        private readonly DbContext context;

        public RolesManagement(DbContext context)
        {
            this.userRoles = context.Set<Role>();
            this.artistRoles = context.Set<RoleArtist>();

            this.context = context;
        }

        public IEnumerable<Role> GetUserRoles()
        {
            return this.userRoles.AsNoTracking();
        }

        public IEnumerable<RoleArtist> GetArtistRoles()
        {
            return this.artistRoles.AsNoTracking();
        }
    }
}
