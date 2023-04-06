using ArenaGestor.BusinessHelpers;
using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.Business
{
    public class SoloistsService : ISoloistsService
    {

        private readonly ISoloistsManagement soloistManagement;
        private readonly IGendersService genderService;
        private readonly IArtistsService artistService;

        public SoloistsService(ISoloistsManagement soloistsManagement, IGendersService genderService, IArtistsService artistService)
        {
            this.soloistManagement = soloistsManagement;
            this.genderService = genderService;
            this.artistService = artistService;
        }

        public Soloist GetSoloistById(int soloistId)
        {
            CommonValidations.ValidId(soloistId);

            Soloist soloist = soloistManagement.GetSoloistById(soloistId);
            if (soloist == null)
            {
                throw new NullReferenceException("The soloist doesn't exists");
            }
            return soloist;
        }

        public IEnumerable<Soloist> GetSoloists(Soloist soloist = null)
        {
            if (soloist != null && !(string.IsNullOrWhiteSpace(soloist.Name)))
            {
                Func<Soloist, bool> filter = new Func<Soloist, bool>(x => x.Name.Trim().ToUpper().Contains(soloist.Name.Trim().ToUpper()));
                return soloistManagement.GetSoloists(filter);
            }
            else
            {
                return soloistManagement.GetSoloists();
            }
        }

        public IEnumerable<Soloist> GetSoloistsByArtist(Artist artist)
        {
            if (artist != null && !(string.IsNullOrWhiteSpace(artist.Name)))
            {
                Func<Soloist, bool> filter = new Func<Soloist, bool>(x => x.Artist.Name.Trim().ToUpper() == artist.Name.Trim().ToUpper());
                return soloistManagement.GetSoloists(filter);
            }
            else
            {
                return new List<Soloist>();
            }
        }

        public Soloist InsertSoloist(Soloist soloist)
        {
            ValidSoloist(soloist);

            Soloist soloistCheckName = new Soloist()
            {
                Name = soloist.Name
            };

            IEnumerable<Soloist> soloists = this.GetSoloists(soloistCheckName);
            if (soloists.Any())
            {
                throw new ArgumentException($"It already exists a musical protagonist with name: {soloistCheckName.Name}");
            }

            soloistManagement.InsertSoloist(soloist);
            soloistManagement.Save();
            return soloist;
        }

        public Soloist UpdateSoloist(Soloist soloist)
        {
            ValidSoloist(soloist);

            Soloist soloistToUpdate = soloistManagement.GetSoloistById(soloist.MusicalProtagonistId);
            if (soloistToUpdate == null)
            {
                throw new NullReferenceException($"The soloist with identifier: {soloist.MusicalProtagonistId} doesn't exists.");

            }
            soloistManagement.UpdateSoloist(soloist);
            soloistManagement.Save();
            return soloistManagement.GetSoloistById(soloist.MusicalProtagonistId);
        }

        public void DeleteSoloist(int musicalProtagonistId)
        {
            CommonValidations.ValidId(musicalProtagonistId);

            Soloist soloistToDelete = soloistManagement.GetSoloistById(musicalProtagonistId);
            if (soloistToDelete == null)
            {
                throw new NullReferenceException($"The soloist with identifier: {musicalProtagonistId} doesn't exists.");
            }
            if (soloistToDelete.Concerts != null && soloistToDelete.Concerts.Any())
            {
                throw new InvalidOperationException("You cannot delete a band with concerts attached");
            }
            soloistManagement.DeleteSoloist(soloistToDelete);
            soloistManagement.Save();
        }

        private void ValidSoloist(Soloist soloist)
        {
            if (soloist == null)
            {
                throw new ArgumentException("You must send the soloist");
            }

            soloist.ValidSoloist();

            var existArtist = artistService.GetArtistById(soloist.ArtistId);

            if (existArtist == null)
            {
                throw new NullReferenceException($"The artist with identifier: {soloist.ArtistId} doesn't exists");
            }

            var existGender = genderService.GetGenderById(soloist.GenderId);

            if (existGender == null)
            {
                throw new NullReferenceException($"The gender with identifier: {soloist.GenderId} doesn't exists");
            }
        }

    }
}
