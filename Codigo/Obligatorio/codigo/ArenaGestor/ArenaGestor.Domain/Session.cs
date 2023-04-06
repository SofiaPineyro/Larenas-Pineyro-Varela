using System;

namespace ArenaGestor.Domain
{
    public class Session
    {
        public int SessionId { get; set; }
        public string Token { get; set; }
        public DateTime Created { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
