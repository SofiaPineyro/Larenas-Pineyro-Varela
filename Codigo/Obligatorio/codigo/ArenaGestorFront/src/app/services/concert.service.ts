import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ConcertGetConcertsDto } from '../models/Concerts/ConcertGetConcertsDto';
import { ConcertInsertConcertDto } from '../models/Concerts/ConcertInsertConcertDto';
import { ConcertResultConcertArtistDto } from '../models/Concerts/ConcertResultConcertArtistDto';
import { ConcertResultConcertDto } from '../models/Concerts/ConcertResultConcertDto';
import { ConcertUpdateConcertDto } from '../models/Concerts/ConcertUpdateConcertDto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConcertService {

  private apiUrl: string

  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiURL + "concerts"
  }

  ParamsFilters(filter?: ConcertGetConcertsDto): string {
    let params = "";

    if (filter) {
      if (filter.tourName.length > 0) {
        params = "?tourName=" + filter.tourName
      }
      if (filter.upcoming) {
        if (params.length > 0) {
          params = params + "&"
        }
        else {
          params = "?"
        }
        params = params + "upcoming=" + filter.upcoming
      }
      if (filter.dateRange && filter.dateRange.startDate && filter.dateRange.endDate) {
        if (params.length > 0) {
          params = params + "&"
        }
        else {
          params = "?"
        }
        params = params + "dateRange.startDate=" + this.ParseDte(filter.dateRange.startDate)
        params = params + "&dateRange.endDate=" + this.ParseDte(filter.dateRange.endDate)
      }
    }
    return params;
  }

  Get(filter: ConcertGetConcertsDto): Observable<Array<ConcertResultConcertDto>> {
    let url = this.apiUrl;

    url = url + this.ParamsFilters(filter);

    return this.http.get<Array<ConcertResultConcertDto>>(url)
  }

  ParseDte(date: Date): string {
    return date.toISOString();
  }

  GetUpcoming(): Observable<Array<ConcertResultConcertDto>> {
    let url = this.apiUrl;
    let params = "?upcoming=" + true;
    url = url + params;

    return this.http.get<Array<ConcertResultConcertDto>>(url)
  }

  GetByArtist(filter?: ConcertGetConcertsDto): Observable<Array<ConcertResultConcertArtistDto>> {
    let params = this.ParamsFilters(filter);

    return this.http.get<Array<ConcertResultConcertArtistDto>>(this.apiUrl + "/ByArtist" + params)
  }

  GetById(id: Number): Observable<ConcertResultConcertDto> {
    return this.http.get<ConcertResultConcertDto>(this.apiUrl + "/" + id.toString())
  }

  Insert(concert: ConcertInsertConcertDto): Observable<ConcertResultConcertDto> {
    return this.http.post<ConcertResultConcertDto>(this.apiUrl, concert)
  }

  Update(concert: ConcertUpdateConcertDto): Observable<ConcertResultConcertDto> {
    return this.http.put<ConcertResultConcertDto>(this.apiUrl, concert)
  }

  Delete(id: Number) {
    return this.http.delete(this.apiUrl + "/" + id.toString())
  }

}
