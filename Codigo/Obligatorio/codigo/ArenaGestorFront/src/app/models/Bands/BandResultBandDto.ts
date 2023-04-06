import { BandResultArtistDto } from "./BandResultArtistDto";
import { BandResultGenderDto } from "./BandResultGenderDto";
import { BandResultConcertDto } from "./BandResultConcertDto";

export class BandResultBandDto {
    musicalProtagonistId: Number = 0;
    name: String = "";
    startDate: Date = new Date();
    gender: BandResultGenderDto = new BandResultGenderDto();
    artists: Array<BandResultArtistDto> = new Array();
    concerts: Array<BandResultConcertDto> = new Array();
}