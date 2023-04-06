using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.DataAccess.Managements
{
    public class MusicalProtagonistManagement : IMusicalProtagonistManagement
    {
        private readonly DbSet<MusicalProtagonist> musicalProtagonist;
        private readonly DbContext context;

        public MusicalProtagonistManagement(DbContext context)
        {
            this.musicalProtagonist = context.Set<MusicalProtagonist>();
            this.context = context;
        }
        
        public MusicalProtagonist GetMusicalProtagonistById(int musicalProtagonistId)
        {
            return musicalProtagonist.Include(x => x.Concerts).AsNoTracking().FirstOrDefault(mp => mp.MusicalProtagonistId == musicalProtagonistId);
        }

        public IEnumerable<MusicalProtagonist> GetMusicalProtagonist(Func<MusicalProtagonist, bool> filter)
        {
            return musicalProtagonist.AsNoTracking().Where(filter);
        }

        public IEnumerable<MusicalProtagonist> GetMusicalProtagonist()
        {
            return musicalProtagonist.AsNoTracking();
        }
    }
}
