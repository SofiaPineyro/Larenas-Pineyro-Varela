using ArenaGestor.BusinessHelpers;
using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.Business
{
    public class ArtistsService : IArtistsService
    {

        private readonly IArtistsManagement artistManagement;
        private readonly IUsersService usersService;

        public ArtistsService(IArtistsManagement artistsManagement, IUsersService usersService)
        {
            this.artistManagement = artistsManagement;
            this.usersService = usersService;
        }

        public Artist GetArtistById(int artistId)
        {
            CommonValidations.ValidId(artistId);
            Artist artist = artistManagement.GetArtistById(artistId);
            if (artist == null)
            {
                throw new NullReferenceException("The artist doesn't exists");
            }

            return artist;
        }

        public IEnumerable<Artist> GetArtists(Artist artist = null)
        {
            if (artist != null && !(string.IsNullOrWhiteSpace(artist.Name)))
            {
                Func<Artist, bool> filter = new Func<Artist, bool>(x => x.Name.Trim().ToUpper().Contains(artist.Name.Trim().ToUpper()));
                return artistManagement.GetArtists(filter);
            }
            else
            {
                return artistManagement.GetArtists();
            }
        }

        public Artist InsertArtist(Artist artist)
        {
            ValidArtist(artist);

            Artist artistCheckName = new Artist()
            {
                Name = artist.Name
            };

            IEnumerable<Artist> artists = this.GetArtists(artistCheckName);
            if (artists.Any())
            {
                throw new ArgumentException($"It already exists an artist with name: {artist.Name}");
            }

            if (artist.UserId.HasValue && artist.UserId > 0)
            {
                var user = usersService.GetUserById(artist.UserId.Value);
                if (!user.Roles.Select(x => x.RoleId).Contains(RoleCode.Artista))
                {
                    throw new NullReferenceException("User is not artist");
                }
            }

            artistManagement.InsertArtist(artist);
            artistManagement.Save();
            return artist;
        }

        public Artist UpdateArtist(Artist artist)
        {
            ValidArtist(artist);

            CommonValidations.ValidId(artist.ArtistId);

            Artist artistToUpdate = artistManagement.GetArtistById(artist.ArtistId);
            if (artistToUpdate == null)
            {
                throw new NullReferenceException($"The artist with identifier: {artist.ArtistId} doesn't exists.");

            }

            if (artist.UserId.HasValue && artist.UserId > 0)
            {
                var user = usersService.GetUserById(artist.UserId.Value);
                if (!user.Roles.Select(x => x.RoleId).Contains(RoleCode.Artista))
                {
                    throw new NullReferenceException("User is not artist");
                }
            }

            artistManagement.UpdateArtist(artist);
            artistManagement.Save();
            return artist;
        }

        public void DeleteArtist(int artistId)
        {
            CommonValidations.ValidId(artistId);

            Artist artistToDelete = artistManagement.GetArtistById(artistId);
            if (artistToDelete == null)
            {
                throw new NullReferenceException($"The artist with identifier: {artistId} doesn't exists.");
            }
            if ((artistToDelete.Soloists != null && artistToDelete.Soloists.Any()) || (artistToDelete.Bands != null && artistToDelete.Bands.Any()))
            {
                throw new InvalidOperationException("You cannot delete an artist with musical protagonists attached");
            }
            artistManagement.DeleteArtist(artistToDelete);
            artistManagement.Save();
        }

        private static void ValidArtist(Artist artist)
        {
            if (artist == null)
            {
                throw new ArgumentException("You must send the artist");
            }

            artist.ValidArtist();
        }

    }
}
