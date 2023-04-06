using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ArenaGestor.DataAccess.Managements
{
    public class TicketStatusManagement : ITicketStatusManagement
    {
        private readonly DbSet<TicketStatus> statuses;
        private readonly DbContext context;

        public TicketStatusManagement(DbContext context)
        {
            this.statuses = context.Set<TicketStatus>();
            this.context = context;
        }

        public TicketStatus GetStatus(TicketCode ticketCode)
        {
            return this.statuses.Where(x => x.TicketStatusId == ticketCode).FirstOrDefault();
        }
    }
}
