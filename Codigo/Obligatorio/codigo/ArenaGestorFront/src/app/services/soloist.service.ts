import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { SoloistResultSoloistDto } from '../models/Soloists/SoloistResultSoloistDto';
import { SoloistGetSoloistsDto } from '../models/Soloists/SoloistGetSoloistsDto';
import { SoloistInsertSoloistDto } from '../models/Soloists/SoloistInsertSoloistDto';
import { SoloistUpdateSoloistDto } from '../models/Soloists/SoloistUpdateSoloistDto';

@Injectable({
  providedIn: 'root'
})
export class SoloistService {

  private apiUrl: string

  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiURL + "soloists"
  }

  Get(filter?: SoloistGetSoloistsDto): Observable<Array<SoloistResultSoloistDto>> {
    let url = this.apiUrl
    if (filter && filter.name.length > 0) {
      url = url + "?name=" + filter.name
    }
    return this.http.get<Array<SoloistResultSoloistDto>>(url)
  }

  GetById(id: Number): Observable<SoloistResultSoloistDto> {
    return this.http.get<SoloistResultSoloistDto>(this.apiUrl + "/" + id.toString())
  }

  Insert(soloist: SoloistInsertSoloistDto): Observable<SoloistResultSoloistDto> {
    return this.http.post<SoloistResultSoloistDto>(this.apiUrl, soloist)
  }

  Update(soloist: SoloistUpdateSoloistDto): Observable<SoloistResultSoloistDto> {
    return this.http.put<SoloistResultSoloistDto>(this.apiUrl, soloist)
  }

  Delete(id: Number) {
    return this.http.delete(this.apiUrl + "/" + id.toString())
  }

}
