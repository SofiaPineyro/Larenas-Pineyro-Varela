import { TicketStatusDto } from "./TicketStatusDto";

export class TicketScanTicketResultDto {
    ticketId: String = "";
    ticketStatus: TicketStatusDto = new TicketStatusDto();
    email: String = "";
    concertId: Number = 0;
}