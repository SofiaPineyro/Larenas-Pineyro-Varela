import { SoloistResultArtistDto } from "./SoloistResultArtistDto";
import { SoloistResultConcertDto } from "./SoloistResultConcertDto";
import { SoloistResultGenderDto } from "./SoloistResultGenderDto";
import { SoloistResultRoleArtistDto } from "./SoloistResultRoleArtistDto";

export class SoloistResultSoloistDto {
    musicalProtagonistId: Number = 0;
    name: String = "";
    startDate: Date = new Date();
    gender: SoloistResultGenderDto = new SoloistResultGenderDto();
    artist: SoloistResultArtistDto = new SoloistResultArtistDto();
    concerts: Array<SoloistResultConcertDto> = new Array();
    roleArtist: SoloistResultRoleArtistDto = new SoloistResultRoleArtistDto();
}