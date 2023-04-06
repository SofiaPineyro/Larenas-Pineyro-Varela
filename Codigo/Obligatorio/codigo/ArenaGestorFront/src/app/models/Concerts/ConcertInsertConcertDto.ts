import { ConcertInsertLocationDto } from "./ConcertInsertLocationDto";
import { ConcertInsertProtagonistDto } from "./ConcertInsertProtagonistDto";

export class ConcertInsertConcertDto {
    tourName: String = "";
    date: Date = new Date();
    ticketCount: Number = 0;
    price: Number = 0;
    protagonists: Array<ConcertInsertProtagonistDto> = new Array();
    location: ConcertInsertLocationDto = new ConcertInsertLocationDto();
}