using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.DataAccess.Managements
{
    public class GendersManagement : IGendersManagement
    {

        private readonly DbSet<Gender> genders;
        private readonly DbContext context;

        public GendersManagement(DbContext context)
        {
            this.genders = context.Set<Gender>();
            this.context = context;
        }

        public void DeleteGender(Gender gender)
        {
            genders.Remove(gender);
        }

        public Gender GetGenderById(int genderId)
        {
            return genders.Include(x => x.MusicalProtagonists).AsNoTracking().FirstOrDefault(gender => gender.GenderId == genderId);
        }

        public IEnumerable<Gender> GetGenders(Func<Gender, bool> filter)
        {
            return genders.Where(filter);
        }

        public IEnumerable<Gender> GetGenders()
        {
            return genders;
        }

        public void InsertGender(Gender gender)
        {
            genders.Add(gender);
        }

        public void UpdateGender(Gender gender)
        {
            genders.Update(gender);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
