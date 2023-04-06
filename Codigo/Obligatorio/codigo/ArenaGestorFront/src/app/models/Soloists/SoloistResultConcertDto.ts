import { SoloistResultLocationDto } from "./SoloistResultLocationDto";

export class SoloistResultConcertDto {
    concertId: Number = 0;
    tourName: String = "";
    date: Date = new Date();
    ticketCount: Number = 0;
    price: Number = 0;
    location: SoloistResultLocationDto = new SoloistResultLocationDto();
}