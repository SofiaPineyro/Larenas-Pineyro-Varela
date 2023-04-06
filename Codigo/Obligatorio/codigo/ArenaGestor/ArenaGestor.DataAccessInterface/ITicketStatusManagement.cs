using ArenaGestor.Domain;

namespace ArenaGestor.DataAccessInterface
{
    public interface ITicketStatusManagement
    {
        TicketStatus GetStatus(TicketCode ticketCode);
    }
}
