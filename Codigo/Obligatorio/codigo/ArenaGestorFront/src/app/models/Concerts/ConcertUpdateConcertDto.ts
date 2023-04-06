import { ConcertUpdateLocationDto } from "./ConcertUpdateLocationDto";
import { ConcertUpdateProtagonistDto } from "./ConcertUpdateProtagonistDto";

export class ConcertUpdateConcertDto {
    concertId: Number = 0;
    tourName: String = "";
    date: Date = new Date();
    ticketCount: Number = 0;
    price: Number = 0;
    protagonists: Array<ConcertUpdateProtagonistDto> = new Array();
    location: ConcertUpdateLocationDto = new ConcertUpdateLocationDto();
}