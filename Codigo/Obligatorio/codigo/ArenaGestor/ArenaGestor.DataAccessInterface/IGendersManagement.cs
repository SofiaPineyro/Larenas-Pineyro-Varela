using ArenaGestor.Domain;
using System;
using System.Collections.Generic;

namespace ArenaGestor.DataAccessInterface
{
    public interface IGendersManagement
    {
        IEnumerable<Gender> GetGenders(Func<Gender, bool> filter);
        IEnumerable<Gender> GetGenders();
        void InsertGender(Gender gender);
        Gender GetGenderById(int genderId);
        void DeleteGender(Gender gender);
        void UpdateGender(Gender gender);
        void Save();
    }
}
