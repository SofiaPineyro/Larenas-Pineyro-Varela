import { TicketConcertDto } from "./TicketConcertDto";
import { TicketStatusDto } from "./TicketStatusDto";

export class TicketGetTicketResultDto {
    ticketId: String = "";
    ticketStatus: TicketStatusDto = new TicketStatusDto();
    email: String = "";
    concertId: Number = 0;
    concert: TicketConcertDto = new TicketConcertDto();
}