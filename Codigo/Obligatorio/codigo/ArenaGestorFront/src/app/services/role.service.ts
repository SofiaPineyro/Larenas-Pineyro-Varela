import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { RolesResultDto } from '../models/Roles/RolesResultDto';
import { Observable } from 'rxjs';
import { RolesArtistResultDto } from '../models/Roles/RolesArtistResultDto';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  private apiUrl: string

  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiURL + "roles"
  }

  GetUserRoles(): Observable<Array<RolesResultDto>> {
    return this.http.get<Array<RolesResultDto>>(this.apiUrl + "/User")
  }

  GetArtistRoles(): Observable<Array<RolesArtistResultDto>> {
    return this.http.get<Array<RolesArtistResultDto>>(this.apiUrl + "/Artist")
  }

}
