using ArenaGestor.Domain;
using System.Collections.Generic;

namespace ArenaGestor.BusinessInterface
{
    public interface IUsersService
    {
        User GetUserById(int userId);
        IEnumerable<User> GetUsers(User user = null);
        User InsertUser(User user);
        User UpdateUser(User user);
        User UpdateUser(string token, User user);
        void DeleteUser(int userId);
        void ChangePassword(UserChangePassword newPassword);
        void ChangePassword(string token, UserChangePassword newPassword);
    }
}
