using ArenaGestor.Domain;
using System;
using System.Collections.Generic;

namespace ArenaGestor.DataAccessInterface
{
    public interface ISessionManagement
    {
        IEnumerable<Session> GetSessions(Func<Session, bool> filter);
        void InsertSession(Session session);
        void DeleteSession(Session session);
        void Save();
    }
}
