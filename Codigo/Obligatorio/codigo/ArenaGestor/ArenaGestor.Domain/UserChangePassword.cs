using ArenaGestor.BusinessHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public class UserChangePassword
    {
        [MaxLength(50)]
        [Required]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }
        [MaxLength(50)]
        [Required]
        public string OldPassword { get; set; }
        [MaxLength(50)]
        [Required]
        public string NewPassword { get; set; }

        public UserChangePassword(string email, string oldPassword, string newPassword)
        {
            Email = email;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }

        public void ValidChangePassword()
        {
            if (!CommonValidations.ValidRequiredString(this.OldPassword))
            {
                throw new ArgumentException("The user old password must have at least one digit");
            }
            if (!CommonValidations.ValidRequiredString(this.NewPassword))
            {
                throw new ArgumentException("The user new password must have at least one digit");
            }
            if (this.OldPassword.Trim().ToUpper() == this.NewPassword.Trim().ToUpper())
            {
                throw new ArgumentException("The password is incorrect");
            }
        }
    }
}
