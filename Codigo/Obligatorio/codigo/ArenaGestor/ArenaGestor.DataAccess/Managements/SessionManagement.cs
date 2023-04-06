using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.DataAccess.Managements
{
    public class SessionManagement : ISessionManagement
    {

        private readonly DbSet<Session> sessions;
        private readonly DbContext context;

        public SessionManagement(DbContext context)
        {
            this.sessions = context.Set<Session>();
            this.context = context;
        }

        public void DeleteSession(Session session)
        {
            sessions.Remove(session);
        }

        public IEnumerable<Session> GetSessions(Func<Session, bool> filter)
        {
            return sessions.Include(x => x.User).ThenInclude(x => x.Roles).ThenInclude(x => x.Role).AsNoTracking().Where(filter);
        }

        public void InsertSession(Session session)
        {
            sessions.Add(session);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
