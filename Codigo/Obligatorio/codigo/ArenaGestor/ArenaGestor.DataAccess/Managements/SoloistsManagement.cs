using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.DataAccess.Managements
{
    public class SoloistsManagement : ISoloistsManagement
    {

        private readonly DbSet<Soloist> soloists;
        private readonly DbContext context;

        public SoloistsManagement(DbContext context)
        {
            this.soloists = context.Set<Soloist>();
            this.context = context;
        }

        public void DeleteSoloist(Soloist soloist)
        {
            soloists.Remove(soloist);
        }

        public Soloist GetSoloistById(int musicalProtagonistId)
        {
            return soloists.Include(x => x.Gender)
                .Include(x => x.Artist)
                .Include(x => x.RoleArtist)
                .Include(x => x.Concerts).ThenInclude(x => x.Concert).ThenInclude(x => x.Location).ThenInclude(x => x.Country)
                .AsNoTracking().FirstOrDefault(soloist => soloist.MusicalProtagonistId == musicalProtagonistId);
        }

        public IEnumerable<Soloist> GetSoloists(Func<Soloist, bool> filter)
        {
            return soloists.Include(x => x.Gender)
                .Include(x => x.Artist)
                .Include(x => x.RoleArtist)
                .Include(x => x.Concerts).ThenInclude(x => x.Concert).ThenInclude(x => x.Location).ThenInclude(x => x.Country)
                .AsNoTracking().Where(filter);
        }

        public IEnumerable<Soloist> GetSoloists()
        {
            return soloists.Include(x => x.Gender)
                .Include(x => x.Artist)
                .Include(x => x.RoleArtist)
                .Include(x => x.Concerts).ThenInclude(x => x.Concert).ThenInclude(x => x.Location).ThenInclude(x => x.Country)
                .AsNoTracking();
        }

        public void InsertSoloist(Soloist soloist)
        {
            soloists.Add(soloist);
        }

        public void UpdateSoloist(Soloist soloist)
        {
            soloists.Update(soloist);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
