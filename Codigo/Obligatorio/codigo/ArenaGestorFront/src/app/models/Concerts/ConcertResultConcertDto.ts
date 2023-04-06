import { ConcertGetMusicalProtagonistDto } from "./ConcertGetMusicalProtagonistDto";
import { ConcertResultLocationDto } from "./ConcertResultLocationDto";

export class ConcertResultConcertDto {
    concertId: Number = 0;
    tourName: String = "";
    date: Date = new Date();
    ticketCount: Number = 0;
    price: Number = 0;
    protagonists: Array<ConcertGetMusicalProtagonistDto> = new Array();
    location: ConcertResultLocationDto = new ConcertResultLocationDto();
}