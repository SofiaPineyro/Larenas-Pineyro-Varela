namespace ArenaGestor.Domain
{
    public class UserRole
    {
        public int UserId { get; set; }
        public RoleCode RoleId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }

        public override bool Equals(object obj)
        {
            return obj is UserRole other && UserId == other.UserId && RoleId == other.RoleId;
        }
    }
}
