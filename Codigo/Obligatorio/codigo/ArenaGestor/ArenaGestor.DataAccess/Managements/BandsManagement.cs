using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.DataAccess.Managements
{
    public class BandsManagement : IBandsManagement
    {

        private readonly DbSet<Band> bands;
        private readonly DbSet<ArtistBand> artists;
        private readonly DbContext context;

        public BandsManagement(DbContext context)
        {
            this.bands = context.Set<Band>();
            this.artists = context.Set<ArtistBand>();
            this.context = context;
        }

        public void DeleteBand(Band band)
        {
            bands.Remove(band);
        }

        public Band GetBandById(int musicalProtagonistId)
        {
            return bands.Include(x => x.Gender).AsNoTracking()
                .Include(x => x.Artists).ThenInclude(x => x.Artist).AsNoTracking()
                .Include(x => x.Artists).ThenInclude(x => x.RoleArtist).AsNoTracking()
                .Include(x => x.Concerts).ThenInclude(x => x.Concert).ThenInclude(x => x.Location).ThenInclude(x => x.Country).AsNoTracking()
                .FirstOrDefault(band => band.MusicalProtagonistId == musicalProtagonistId);
        }

        public IEnumerable<Band> GetBands(Func<Band, bool> filter)
        {
            return bands.Include(x => x.Gender).AsNoTracking()
                .Include(x => x.Artists).ThenInclude(x => x.Artist).AsNoTracking()
                .Include(x => x.Artists).ThenInclude(x => x.RoleArtist).AsNoTracking()
                .Include(x => x.Concerts).ThenInclude(x => x.Concert).ThenInclude(x => x.Location).ThenInclude(x => x.Country).AsNoTracking()
                .Where(filter);
        }

        public IEnumerable<Band> GetBands()
        {
            return bands.Include(x => x.Gender).AsNoTracking()
                .Include(x => x.Artists).ThenInclude(x => x.Artist).AsNoTracking()
                .Include(x => x.Artists).ThenInclude(x => x.RoleArtist).AsNoTracking()
                .Include(x => x.Concerts).ThenInclude(x => x.Concert).ThenInclude(x => x.Location).ThenInclude(x => x.Country).AsNoTracking();
        }

        public void InsertBand(Band band)
        {
            bands.Add(band);
        }

        public void UpdateBand(Band band)
        {
            List<ArtistBand> artistsInDB = artists.AsNoTracking()
                .Include(x => x.RoleArtist).AsNoTracking()
                .Where(r => r.MusicalProtagonistId == band.MusicalProtagonistId).ToList();

            if (band != null && band.Artists != null && band.Artists.Any())
            {
                foreach (ArtistBand artist in artistsInDB)
                {
                    context.Entry<ArtistBand>(artist).State = EntityState.Deleted;
                }
            }
            bands.Update(band);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
