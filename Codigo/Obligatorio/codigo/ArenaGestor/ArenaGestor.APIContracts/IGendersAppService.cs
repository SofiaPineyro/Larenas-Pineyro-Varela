using ArenaGestor.APIContracts.Gender;
using Microsoft.AspNetCore.Mvc;

namespace ArenaGestor.APIContracts
{
    public interface IGendersAppService
    {
        IActionResult GetGenderById(int genderId);
        IActionResult GetGenders(GenderGetGendersDto genderFilter = null);
        IActionResult PostGender(GenderInsertGenderDto insertGender);
        IActionResult PutGender(GenderUpdateGenderDto updateGender);
        IActionResult DeleteGender(int genderId);
    }
}
