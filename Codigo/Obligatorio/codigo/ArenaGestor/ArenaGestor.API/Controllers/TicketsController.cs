using ArenaGestor.API.Filters;
using ArenaGestor.APIContracts;
using ArenaGestor.APIContracts.Ticket;
using ArenaGestor.BusinessInterface;
using ArenaGestor.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ArenaGestor.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class TicketsController : ControllerBase, ITicketAppService
    {

        private readonly ITicketService ticketService;
        private readonly IMapper mapper;

        public TicketsController(ITicketService ticketService, IMapper mapper)
        {
            this.ticketService = ticketService;
            this.mapper = mapper;
        }

        [AuthorizationFilter(RoleCode.Vendedor)]
        [HttpPost("Sale")]
        public IActionResult PostTickets([FromBody] TicketSellTicketDto sellTicket)
        {
            var ticketSell = mapper.Map<TicketSell>(sellTicket);
            Ticket ticket = ticketService.SellTicket(ticketSell);
            var resultDto = mapper.Map<TicketSellTicketResultDto>(ticket);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Espectador)]
        [HttpPost("Shopping")]
        public IActionResult PostTickets([FromBody] TicketBuyTicketDto buyTicket)
        {
            var token = this.HttpContext.Request.Headers["token"];

            var ticketBuy = mapper.Map<TicketBuy>(buyTicket);
            Ticket ticket = ticketService.BuyTicket(token, ticketBuy);
            var resultDto = mapper.Map<TicketBuyTicketResultDto>(ticket);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Acomodador)]
        [HttpPut]
        public IActionResult PutTickets([FromBody] TicketScanTicketDto scanTicket)
        {
            Ticket ticket = ticketService.ScanTicket(scanTicket.TicketId);
            var resultDto = mapper.Map<TicketScanTicketResultDto>(ticket);
            return Ok(resultDto);
        }

        [AuthorizationFilter(RoleCode.Espectador)]
        [HttpGet]
        public IActionResult GetTickets()
        {
            var token = this.HttpContext.Request.Headers["token"];
            var tickets = ticketService.GetTicketsByUser(token);
            var resultDto = mapper.Map<IEnumerable<TicketGetTicketResultDto>>(tickets);
            return Ok(resultDto);
        }

    }
}
