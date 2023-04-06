using ArenaGestor.BusinessHelpers;
using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using System;
using System.Collections.Generic;

namespace ArenaGestor.Business
{
    public class MusicalProtagonistService : IMusicalProtagonistService
    {
        private readonly IMusicalProtagonistManagement musicalProtagonistManagement;
        public MusicalProtagonistService(IMusicalProtagonistManagement musicalProtagonistManagement)
        {
            this.musicalProtagonistManagement = musicalProtagonistManagement;
        }

        public IEnumerable<MusicalProtagonist> GetMusicalProtagonist(MusicalProtagonist musicalProtagonist = null)
        {
            if (musicalProtagonist != null && !(string.IsNullOrWhiteSpace(musicalProtagonist.Name)))
            {
                Func<MusicalProtagonist, bool> filter = new Func<MusicalProtagonist, bool>(x => x.Name.Trim().ToUpper() == musicalProtagonist.Name.Trim().ToUpper());
                return musicalProtagonistManagement.GetMusicalProtagonist(filter);
            }
            else
            {
                return musicalProtagonistManagement.GetMusicalProtagonist();
            }
        }

        public MusicalProtagonist GetMusicalProtagonistById(int musicalProtagonistId)
        {
            CommonValidations.ValidId(musicalProtagonistId);

            MusicalProtagonist protagonist = musicalProtagonistManagement.GetMusicalProtagonistById(musicalProtagonistId);
            if (protagonist == null)
            {
                throw new NullReferenceException("The protagonist doesn't exists");
            }
            return protagonist;
        }
    }
}
