using ArenaGestor.BusinessHelpers;
using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.Business
{
    public class GendersService : IGendersService
    {

        private readonly IGendersManagement genderManagement;
        public GendersService(IGendersManagement gendersManagement)
        {
            this.genderManagement = gendersManagement;
        }

        public Gender GetGenderById(int genderId)
        {
            CommonValidations.ValidId(genderId);
            Gender gender = genderManagement.GetGenderById(genderId);
            if (gender == null)
            {
                throw new NullReferenceException("The gender doesn't exists");
            }
            return gender;
        }

        public IEnumerable<Gender> GetGenders(Gender gender = null)
        {
            if (gender != null && !(string.IsNullOrWhiteSpace(gender.Name)))
            {
                Func<Gender, bool> filter = new Func<Gender, bool>(x => x.Name.Trim().ToUpper().Contains(gender.Name.Trim().ToUpper()));
                return genderManagement.GetGenders(filter);
            }
            else
            {
                return genderManagement.GetGenders();
            }
        }

        public Gender InsertGender(Gender gender)
        {
            ValidGender(gender);

            Gender genderCheckname = new Gender()
            {
                Name = gender.Name
            };

            IEnumerable<Gender> genders = this.GetGenders(genderCheckname);
            if (genders.Any())
            {
                throw new ArgumentException($"It already exists a gender with name: {gender.Name}");
            }

            genderManagement.InsertGender(gender);
            genderManagement.Save();
            return gender;
        }

        public Gender UpdateGender(Gender gender)
        {
            ValidGender(gender);

            CommonValidations.ValidId(gender.GenderId);

            Gender genderToUpdate = genderManagement.GetGenderById(gender.GenderId);
            if (genderToUpdate == null)
            {
                throw new NullReferenceException($"The gender with identifier: {gender.GenderId} doesn't exists.");

            }
            genderManagement.UpdateGender(gender);
            genderManagement.Save();
            return gender;
        }

        public void DeleteGender(int genderId)
        {
            CommonValidations.ValidId(genderId);

            Gender genderToDelete = genderManagement.GetGenderById(genderId);
            if (genderToDelete == null)
            {
                throw new NullReferenceException($"The gender with identifier: {genderId} doesn't exists.");
            }

            if (genderToDelete.MusicalProtagonists != null && genderToDelete.MusicalProtagonists.Any())
            {
                throw new InvalidOperationException("You cannot delete a gender with musical protagonists attached");
            }

            genderManagement.DeleteGender(genderToDelete);
            genderManagement.Save();
        }

        private static void ValidGender(Gender gender)
        {
            if (gender == null)
            {
                throw new ArgumentException("You must send the gender");
            }

            gender.ValidGender();
        }

    }
}
