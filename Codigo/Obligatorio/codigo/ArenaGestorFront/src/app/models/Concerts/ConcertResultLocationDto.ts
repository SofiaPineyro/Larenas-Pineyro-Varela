import { ConcertResultCountryDto } from "./ConcertResultCountryDto";

export class ConcertResultLocationDto {
    locationId: Number = 0;
    place: String = "";
    street: String = "";
    number: Number = 0;
    country: ConcertResultCountryDto = new ConcertResultCountryDto();
}