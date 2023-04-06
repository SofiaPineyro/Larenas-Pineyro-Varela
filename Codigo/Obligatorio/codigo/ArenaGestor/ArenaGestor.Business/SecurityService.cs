using ArenaGestor.BusinessHelpers;
using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using System;
using System.Linq;

namespace ArenaGestor.Business
{
    public class SecurityService : ISecurityService
    {

        private readonly ISessionManagement sessionManagement;
        private readonly IUsersManagement usersManagement;

        public SecurityService(ISessionManagement sessionManagement, IUsersManagement usersManagement)
        {
            this.sessionManagement = sessionManagement;
            this.usersManagement = usersManagement;
        }

        public User GetUserOfToken(string token)
        {
            ValidToken(token);

            Func<Session, bool> filter = new Func<Session, bool>(x => x.Token == token);
            Session session = sessionManagement.GetSessions(filter).FirstOrDefault();
            if (session == null)
            {
                throw new ArgumentException("The token doesn't exists");
            }
            return session.User;
        }

        public string Login(string email, string password)
        {
            ValidLogin(email, password);

            Func<User, bool> filter = new Func<User, bool>(x => x.Email.ToLower() == email.ToLower() && x.Password == password);
            User user = usersManagement.GetUsers(filter).FirstOrDefault();
            if (user == null)
            {
                throw new NullReferenceException("The email or password is incorrect");
            }
            Session session = new Session
            {
                Token = BusinessHelpers.StringGenerator.GenerateRandomToken(64),
                Created = DateTime.Now,
                UserId = user.UserId
            };
            sessionManagement.InsertSession(session);
            sessionManagement.Save();
            return session.Token;
        }

        public void Logout(string token)
        {
            ValidToken(token);

            Func<Session, bool> filter = new Func<Session, bool>(x => x.Token == token);
            Session sessionToDelete = sessionManagement.GetSessions(filter).FirstOrDefault();
            if (sessionToDelete == null)
            {
                throw new ArgumentException("The token doesn't exists");
            }
            sessionManagement.DeleteSession(sessionToDelete);
            sessionManagement.Save();
        }

        public bool UserHaveRole(RoleCode role, string token)
        {
            ValidToken(token);

            User user = GetUserOfToken(token);
            return user.Roles.Where(x => x.RoleId == role).Any();
        }

        private void ValidLogin(string email, string password)
        {
            if (!CommonValidations.ValidRequiredString(email))
            {
                throw new ArgumentException("The user email is required");
            }
            if (!CommonValidations.ValidEmail(email))
            {
                throw new ArgumentException("The user email is invalid");
            }
            if (!CommonValidations.ValidRequiredString(password))
            {
                throw new ArgumentException("The user password must have at least one digit");
            }
        }

        private void ValidToken(string token)
        {
            if (!CommonValidations.ValidRequiredString(token))
            {
                throw new ArgumentException("Invalid Token");
            }
        }
    }
}
