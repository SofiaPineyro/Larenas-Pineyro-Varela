using ArenaGestor.BusinessHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public class User
    {
        public int UserId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string Surname { get; set; }

        [MaxLength(50)]
        [Required]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }

        [MaxLength(50)]
        [Required]
        public string Password { get; set; }

        public ICollection<UserRole> Roles { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User other
                && (UserId == other.UserId ||
                (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(other.Email) && Email.Trim().ToUpper() == other.Email.Trim().ToUpper()));
        }

        public void ValidUser()
        {
            if (!CommonValidations.ValidRequiredString(this.Name))
            {
                throw new ArgumentException("The user name is required");
            }
            if (!CommonValidations.ValidRequiredString(this.Surname))
            {
                throw new ArgumentException("The user surname is required");
            }
            if (!CommonValidations.ValidRequiredString(this.Email))
            {
                throw new ArgumentException("The user email is required");
            }
            if (!CommonValidations.ValidEmail(this.Email))
            {
                throw new ArgumentException("The user email is invalid");
            }
            if (!CommonValidations.ValidRequiredString(this.Password))
            {
                throw new ArgumentException("The user password must have at least one digit");
            }
        }
    }
}
