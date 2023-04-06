import { BandResultLocationDto } from "./BandResultLocationDto";

export class BandResultConcertDto {
    concertId: Number = 0;
    tourName: String = "";
    date: Date = new Date();
    ticketCount: Number = 0;
    price: Number = 0;
    location: BandResultLocationDto = new BandResultLocationDto();
}