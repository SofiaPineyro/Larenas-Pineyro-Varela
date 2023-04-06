import { BandUpdateArtistDto } from "./BandUpdateArtistDto";

export class BandUpdateBandDto {
    musicalProtagonistId: Number = 0;
    name: String = "";
    startDate: Date = new Date();
    genderId: Number = 0;
    artists: Array<BandUpdateArtistDto> = new Array();
}