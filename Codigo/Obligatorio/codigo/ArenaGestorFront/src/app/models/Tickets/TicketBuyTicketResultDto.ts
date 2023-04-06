import { TicketStatusDto } from "./TicketStatusDto";

export class TicketBuyTicketResultDto {
    ticketId: String = "";
    ticketStatus: TicketStatusDto = new TicketStatusDto();
    email: String = "";
    concertId: Number = 0;
}