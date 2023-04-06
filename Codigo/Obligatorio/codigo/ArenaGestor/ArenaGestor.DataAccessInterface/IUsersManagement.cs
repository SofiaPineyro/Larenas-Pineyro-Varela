using ArenaGestor.Domain;
using System;
using System.Collections.Generic;

namespace ArenaGestor.DataAccessInterface
{
    public interface IUsersManagement
    {
        IEnumerable<User> GetUsers(Func<User, bool> filter);
        IEnumerable<User> GetUsers();
        void InsertUser(User user);
        User GetUserById(int userId);
        void DeleteUser(User user);
        void UpdateUser(User user);
        public void UpdateUserHeader(User user);
        void Save();
    }
}
