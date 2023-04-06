import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BandGetBandsDto } from '../models/Bands/BandGetBandsDto';
import { BandInsertBandDto } from '../models/Bands/BandInsertBandDto';
import { BandResultBandDto } from '../models/Bands/BandResultBandDto';
import { BandUpdateBandDto } from '../models/Bands/BandUpdateBandDto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BandService {

  private apiUrl: string

  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiURL + "bands"
  }

  Get(filter?: BandGetBandsDto): Observable<Array<BandResultBandDto>> {
    let url = this.apiUrl
    if (filter && filter.name.length > 0) {
      url = url + "?name=" + filter.name
    }
    return this.http.get<Array<BandResultBandDto>>(url)
  }

  GetById(id: Number): Observable<BandResultBandDto> {
    return this.http.get<BandResultBandDto>(this.apiUrl + "/" + id.toString())
  }

  Insert(band: BandInsertBandDto): Observable<BandResultBandDto> {
    return this.http.post<BandResultBandDto>(this.apiUrl, band)
  }

  Update(band: BandUpdateBandDto): Observable<BandResultBandDto> {
    return this.http.put<BandResultBandDto>(this.apiUrl, band)
  }

  Delete(id: Number) {
    return this.http.delete(this.apiUrl + "/" + id.toString())
  }

}
