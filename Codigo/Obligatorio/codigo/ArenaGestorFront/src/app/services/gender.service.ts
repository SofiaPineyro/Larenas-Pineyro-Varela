import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { GenderResultGenderDto } from '../models/Genders/GenderResultGenderDto';
import { GenderInsertGenderDto } from '../models/Genders/GenderInsertGenderDto';
import { GenderUpdateGenderDto } from '../models/Genders/GenderUpdateGenderDto';
import { GenderGetGendersDto } from '../models/Genders/GenderGetGendersDto';

@Injectable({
  providedIn: 'root'
})
export class GenderService {

  private apiUrl: string

  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiURL + "genders"
  }

  Get(filter?: GenderGetGendersDto): Observable<Array<GenderResultGenderDto>> {
    let url = this.apiUrl
    if (filter && filter.name.length > 0) {
      url = url + "?name=" + filter.name
    }
    return this.http.get<Array<GenderResultGenderDto>>(url)
  }

  GetById(id: Number): Observable<GenderResultGenderDto> {
    return this.http.get<GenderResultGenderDto>(this.apiUrl + "/" + id.toString())
  }

  Insert(gender: GenderInsertGenderDto): Observable<GenderResultGenderDto> {
    return this.http.post<GenderResultGenderDto>(this.apiUrl, gender)
  }

  Update(gender: GenderUpdateGenderDto): Observable<GenderResultGenderDto> {
    return this.http.put<GenderResultGenderDto>(this.apiUrl, gender)
  }

  Delete(id: Number) {
    return this.http.delete(this.apiUrl + "/" + id.toString())
  }

}
