using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.DataAccess.Managements
{
    public class UsersManagement : IUsersManagement
    {

        private readonly DbSet<User> users;
        private readonly DbSet<UserRole> roles;
        private readonly DbContext context;

        public UsersManagement(DbContext context)
        {
            this.users = context.Set<User>();
            this.roles = context.Set<UserRole>();
            this.context = context;
        }

        public User GetUserById(int userId)
        {
            return users.Include(x => x.Roles).ThenInclude(ur => ur.Role).AsNoTracking().FirstOrDefault(x => x.UserId == userId);
        }

        public IEnumerable<User> GetUsers(Func<User, bool> filter)
        {
            return users.AsNoTracking().Where(filter);
        }

        public IEnumerable<User> GetUsers()
        {
            return users.AsNoTracking();
        }

        public void InsertUser(User user)
        {
            foreach (UserRole role in user.Roles)
            {
                if (role.Role != null)
                {
                    context.Attach(role.Role);
                }
            }
            users.Add(user);
        }

        public void UpdateUser(User user)
        {

            List<UserRole> rolesInDB = roles.AsNoTracking().Where(r => r.UserId == user.UserId).ToList();

            foreach (UserRole role in rolesInDB)
            {
                context.Entry<UserRole>(role).State = EntityState.Deleted;
            }
            this.context.SaveChanges();
            users.Update(user);

        }
        public void UpdateUserHeader(User user)
        {
            users.Update(user);
        }

        public void DeleteUser(User user)
        {
            users.Remove(user);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
