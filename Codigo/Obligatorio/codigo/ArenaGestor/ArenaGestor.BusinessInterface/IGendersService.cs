using ArenaGestor.Domain;
using System.Collections.Generic;

namespace ArenaGestor.BusinessInterface
{
    public interface IGendersService
    {
        Gender GetGenderById(int genderId);
        IEnumerable<Gender> GetGenders(Gender gender = null);
        Gender InsertGender(Gender gender);
        Gender UpdateGender(Gender gender);
        void DeleteGender(int genderId);
    }
}
