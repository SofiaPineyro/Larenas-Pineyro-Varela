using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.DataAccess.Managements
{
    public class ArtistsManagement : IArtistsManagement
    {

        private readonly DbSet<Artist> artists;
        private readonly DbContext context;

        public ArtistsManagement(DbContext context)
        {
            this.artists = context.Set<Artist>();
            this.context = context;
        }

        public void DeleteArtist(Artist artist)
        {
            artists.Remove(artist);
        }

        public Artist GetArtistById(int artistId)
        {
            return artists.Include(x => x.Bands).Include(x => x.Soloists).Include(x => x.User).AsNoTracking().FirstOrDefault(artist => artist.ArtistId == artistId);
        }

        public IEnumerable<Artist> GetArtists(Func<Artist, bool> filter)
        {
            return artists.Include(x => x.Bands).Include(x => x.Soloists).Include(x => x.User).Where(filter).OrderBy(x => x.ArtistId);
        }

        public IEnumerable<Artist> GetArtists()
        {
            return artists.AsNoTracking().Include(x => x.Bands).Include(x => x.Soloists).Include(x => x.User).AsNoTracking().OrderBy(x => x.ArtistId);
        }

        public void InsertArtist(Artist artist)
        {
            artist.User = null;
            artists.Add(artist);
        }

        public void UpdateArtist(Artist artist)
        {
            foreach (ArtistBand band in artist.Bands)
            {
                context.Entry(band).State = EntityState.Unchanged;
            }
            foreach (Soloist soloist in artist.Soloists)
            {
                context.Entry(soloist).State = EntityState.Unchanged;
            }

            artists.Update(artist);
            context.Entry(artist).Collection(x => x.Bands).IsModified = false;
            context.Entry(artist).Collection(x => x.Soloists).IsModified = false;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
