using ArenaGestor.BusinessHelpers;
using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.Business
{
    public class BandsService : IBandsService
    {

        private readonly IBandsManagement bandManagement;
        private readonly IGendersService genderService;
        private readonly IArtistsService artistService;

        public BandsService(IBandsManagement bandsManagement, IGendersService genderService, IArtistsService artistService)
        {
            this.bandManagement = bandsManagement;
            this.genderService = genderService;
            this.artistService = artistService;
        }

        public Band GetBandById(int bandId)
        {
            CommonValidations.ValidId(bandId);

            Band band = bandManagement.GetBandById(bandId);
            if (band == null)
            {
                throw new NullReferenceException("The band doesn't exists");
            }
            return band;
        }

        public IEnumerable<Band> GetBands(Band band = null)
        {
            if (band != null && !(string.IsNullOrWhiteSpace(band.Name)))
            {
                Func<Band, bool> filter = new Func<Band, bool>(x => x.Name.Trim().ToUpper().Contains(band.Name.Trim().ToUpper()));
                return bandManagement.GetBands(filter);
            }
            else
            {
                return bandManagement.GetBands();
            }
        }

        public IEnumerable<Band> GetBandsByArtist(Artist artist)
        {
            if (artist != null && !(string.IsNullOrWhiteSpace(artist.Name)))
            {
                Func<Band, bool> filter = new Func<Band, bool>(x => x.Artists.Select(s => s.Artist).Contains(artist));
                return bandManagement.GetBands(filter);
            }
            else
            {
                return new List<Band>();
            }
        }

        public Band InsertBand(Band band)
        {
            ValidBand(band);

            Band bandCheckName = new Band()
            {
                Name = band.Name
            };

            IEnumerable<Band> bands = this.GetBands(bandCheckName);
            if (bands.Any())
            {
                throw new ArgumentException($"It already exists an musical protagonist with name: {band.Name}");
            }

            bandManagement.InsertBand(band);
            bandManagement.Save();
            return band;
        }

        public Band UpdateBand(Band band)
        {
            ValidBand(band);

            Band bandToUpdate = bandManagement.GetBandById(band.MusicalProtagonistId);
            if (bandToUpdate == null)
            {
                throw new NullReferenceException($"The band with identifier: {band.MusicalProtagonistId} doesn't exists.");

            }
            bandManagement.UpdateBand(band);
            bandManagement.Save();
            return bandManagement.GetBandById(band.MusicalProtagonistId);
        }

        public void DeleteBand(int bandId)
        {
            CommonValidations.ValidId(bandId);

            Band bandToDelete = bandManagement.GetBandById(bandId);
            if (bandToDelete == null)
            {
                throw new NullReferenceException($"The band with identifier: {bandId} doesn't exists.");
            }

            if (bandToDelete.Concerts != null && bandToDelete.Concerts.Any())
            {
                throw new InvalidOperationException("You cannot delete a band with concerts attached");
            }

            bandManagement.DeleteBand(bandToDelete);
            bandManagement.Save();
        }

        private void ValidBand(Band band)
        {
            if (band == null)
            {
                throw new ArgumentException("You must send the band");
            }

            band.ValidBand();

            foreach (var artist in band.Artists)
            {
                var existArtis = artistService.GetArtistById(artist.ArtistId);

                if (existArtis == null)
                {
                    throw new NullReferenceException($"The artist with identifier: {artist.ArtistId} doesn't exists");
                }
            }

            var existGender = genderService.GetGenderById(band.GenderId);

            if (existGender == null)
            {
                throw new NullReferenceException($"The gender with identifier: {band.GenderId} doesn't exists");
            }
        }

    }
}
