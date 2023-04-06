using ArenaGestor.Domain;

namespace ArenaGestor.BusinessInterface
{
    public interface ISecurityService
    {
        User GetUserOfToken(string token);
        bool UserHaveRole(RoleCode role, string token);
        string Login(string email, string password);
        void Logout(string token);
    }
}
