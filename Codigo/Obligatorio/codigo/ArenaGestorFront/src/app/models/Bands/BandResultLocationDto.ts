import { BandResultCountryDto } from "./BandResultCountryDto";

export class BandResultLocationDto {
    locationId: Number = 0;
    place: String = "";
    street: String = "";
    number: Number = 0;
    country: BandResultCountryDto = new BandResultCountryDto();
}