import { TicketStatusDto } from "./TicketStatusDto";

export class TicketSellTicketResultDto {
    ticketId: String = "";
    ticketStatus: TicketStatusDto = new TicketStatusDto();
    email: String = "";
    concertId: Number = 0;
}