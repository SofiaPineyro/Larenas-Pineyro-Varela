import { ConcertGetDateRangeConcertsDto } from "./ConcertGetDateRangeConcertsDto";

export class ConcertGetConcertsDto {
    tourName: String = "";
    upcoming: Boolean = false;
    dateRange: ConcertGetDateRangeConcertsDto = new ConcertGetDateRangeConcertsDto();
}