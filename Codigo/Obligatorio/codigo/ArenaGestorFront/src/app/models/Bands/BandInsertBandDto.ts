import { BandInsertArtistDto } from "./BandInsertArtistDto";

export class BandInsertBandDto {
    name: String = "";
    startDate: Date = new Date();
    genderId: Number = 0;
    artists: Array<BandInsertArtistDto> = new Array();
}