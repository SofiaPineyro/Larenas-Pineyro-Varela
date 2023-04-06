import { BandResultRoleArtistDto } from "../Bands/BandResultRoleArtistDto";

export class BandResultArtistDto {
    artistId: Number = 0;
    name: String = "";
    roleArtist: BandResultRoleArtistDto = new BandResultRoleArtistDto();
}