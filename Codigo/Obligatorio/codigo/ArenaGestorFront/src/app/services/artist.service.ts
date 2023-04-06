import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ArtistResultArtistDto } from '../models/Artists/ArtistResultArtistDto';
import { ArtistGetArtistsDto } from '../models/Artists/ArtistGetArtistsDto';
import { ArtistInsertArtistDto } from '../models/Artists/ArtistInsertArtistDto';
import { ArtistUpdateArtistDto } from '../models/Artists/ArtistUpdateArtistDto';

@Injectable({
  providedIn: 'root'
})
export class ArtistService {

  private apiUrl: string

  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiURL + "artists"
  }

  Get(filter?: ArtistGetArtistsDto): Observable<Array<ArtistResultArtistDto>> {
    let url = this.apiUrl
    if (filter && filter.name.length > 0) {
      url = url + "?name=" + filter.name
    }
    return this.http.get<Array<ArtistResultArtistDto>>(url)
  }

  GetById(id: Number): Observable<ArtistResultArtistDto> {
    return this.http.get<ArtistResultArtistDto>(this.apiUrl + "/" + id.toString())
  }

  Insert(artist: ArtistInsertArtistDto): Observable<ArtistResultArtistDto> {
    return this.http.post<ArtistResultArtistDto>(this.apiUrl, artist)
  }

  Update(artist: ArtistUpdateArtistDto): Observable<ArtistResultArtistDto> {
    return this.http.put<ArtistResultArtistDto>(this.apiUrl, artist)
  }

  Delete(id: Number) {
    return this.http.delete(this.apiUrl + "/" + id.toString())
  }

}
