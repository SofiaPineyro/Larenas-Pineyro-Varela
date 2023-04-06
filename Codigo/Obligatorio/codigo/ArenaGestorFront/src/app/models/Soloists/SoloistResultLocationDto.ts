import { SoloistResultCountryDto } from "./SoloistResultCountryDto";

export class SoloistResultLocationDto {
    locationId: Number = 0;
    place: String = "";
    street: String = "";
    number: Number = 0;
    country: SoloistResultCountryDto = new SoloistResultCountryDto();
}