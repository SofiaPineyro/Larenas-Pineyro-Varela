using ArenaGestor.BusinessHelpers;
using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.Business
{
    public class UsersService : IUsersService
    {
        private readonly IUsersManagement UserManagement;
        private ISecurityService securityService;

        public UsersService(IUsersManagement UsersManagement, ISecurityService securityService)
        {
            this.UserManagement = UsersManagement;
            this.securityService = securityService;
        }

        public User GetUserById(int userId)
        {
            CommonValidations.ValidId(userId);

            User user = UserManagement.GetUserById(userId);
            if (user == null)
            {
                throw new NullReferenceException("The user doesn't exists");
            }
            return user;
        }

        public IEnumerable<User> GetUsers(User user = null)
        {
            if (user == null) { return UserManagement.GetUsers(); }

            List<User> users = new List<User>();

            if (CommonValidations.ValidRequiredString(user.Name))
            {
                var name = user.Name.Trim().ToUpper();
                Func<User, bool> filter = new Func<User, bool>(x => x.Name.Trim().ToUpper().Contains(name));

                users = UserManagement.GetUsers(filter).ToList();
            }
            else
            {
                users = UserManagement.GetUsers().ToList();
            }

            if (CommonValidations.ValidRequiredString(user.Surname))
            {
                var surname = user.Surname.Trim().ToUpper();
                users = users.Where(x => x.Surname.Trim().ToUpper().Contains(surname)).ToList(); ;
            }

            if (CommonValidations.ValidRequiredString(user.Email))
            {
                var email = user.Email.Trim().ToUpper();
                users = users.Where(x => x.Email.Trim().ToUpper().Contains(email)).ToList(); ;
            }

            return users;
        }

        public User InsertUser(User user)
        {
            ValidUser(user);

            if (!user.Roles.Any())
            {
                throw new ArgumentException("The user must have at least one role");
            }

            Func<User, bool> filter = new Func<User, bool>(x => x.Email.Trim().ToUpper() == user.Email.Trim().ToUpper());
            if (UserManagement.GetUsers(filter).FirstOrDefault() != null)
            {
                throw new ArgumentException("It already exists a user with that email");
            }

            user.Email = user.Email.ToLower();
            UserManagement.InsertUser(user);
            UserManagement.Save();
            return user;
        }

        public User UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentException($"The user cannot be null");
            }
            User userToUpdate = UserManagement.GetUserById(user.UserId);

            if (userToUpdate == null)
            {
                throw new NullReferenceException($"The user with identifier: {user.UserId} doesn't exists.");
            }

            user.Email = userToUpdate.Email;
            user.Password = userToUpdate.Password;

            ValidUser(user);

            UserManagement.UpdateUser(user);
            UserManagement.Save();
            return user;
        }

        public User UpdateUser(string token, User user)
        {
            if (user == null)
            {
                throw new ArgumentException($"The user cannot be null");
            }

            var userToUpdate = securityService.GetUserOfToken(token);

            if (userToUpdate == null)
            {
                throw new NullReferenceException($"The user with identifier: {user.UserId} doesn't exists.");
            }

            user.Email = userToUpdate.Email;
            user.Password = userToUpdate.Password;

            ValidUser(user);

            UserManagement.UpdateUser(user);
            UserManagement.Save();
            return user;
        }

        public void DeleteUser(int userId)
        {
            CommonValidations.ValidId(userId);

            User userToDelete = UserManagement.GetUserById(userId);
            if (userToDelete == null)
            {
                throw new NullReferenceException($"The user with identifier: {userId} doesn't exists.");
            }
            UserManagement.DeleteUser(userToDelete);
            UserManagement.Save();
        }

        public void ChangePassword(UserChangePassword newPassword)
        {
            if (newPassword == null)
            {
                throw new ArgumentException("You must send the new password");
            }

            if (!CommonValidations.ValidRequiredString(newPassword.Email))
            {
                throw new ArgumentException("The user email is required");
            }
            newPassword.ValidChangePassword();

            Func<User, bool> filter = new Func<User, bool>(x => x.Email.Trim().ToUpper() == newPassword.Email.Trim().ToUpper());
            if (!UserManagement.GetUsers(filter).Any())
            {
                throw new NullReferenceException("It doesn't exists a user with that email");
            }

            User user = UserManagement.GetUsers(filter).FirstOrDefault();

            if (newPassword.OldPassword.Trim().ToUpper() != user.Password.Trim().ToUpper())
            {
                throw new ArgumentException("The old password is incorrect");
            }

            user.Password = newPassword.NewPassword;
            UserManagement.UpdateUserHeader(user);
            UserManagement.Save();
        }

        public void ChangePassword(string token, UserChangePassword newPassword)
        {
            if (newPassword == null)
            {
                throw new ArgumentException("You must send the new password");
            }

            newPassword.ValidChangePassword();

            var user = securityService.GetUserOfToken(token);

            if (user == null)
            {
                throw new NullReferenceException("User is not logged in");
            }

            if (newPassword.OldPassword.Trim().ToUpper() != user.Password.Trim().ToUpper())
            {
                throw new ArgumentException("The old password is incorrect");
            }

            user.Password = newPassword.NewPassword;
            UserManagement.UpdateUserHeader(user);
            UserManagement.Save();
        }

        private static void ValidUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentException("You must send the user");
            }

            user.ValidUser();
        }

    }
}
