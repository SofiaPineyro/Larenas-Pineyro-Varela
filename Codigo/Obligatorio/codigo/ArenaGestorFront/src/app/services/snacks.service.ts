import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { SnackResultDto } from '../models/Snacks/SnackResultDto';
import { SnackInsertDto } from '../models/Snacks/SnackInsertDto';

@Injectable({
  providedIn: 'root',
})
export class SnacksService {
  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiURL + 'snacks';
  }

  Get(): Observable<Array<SnackResultDto>> {
    return this.http.get<Array<SnackResultDto>>(this.apiUrl);
  }

  Buy(snack: { id: Number; quantity: Number }): Observable<SnackResultDto> {
    return this.http.post<SnackResultDto>(this.apiUrl + '/buy', snack);
  }

  Insert(snack: SnackInsertDto): Observable<SnackResultDto> {
    return this.http.post<SnackResultDto>(this.apiUrl, snack);
  }

  Delete(id: Number) {
    return this.http.delete(this.apiUrl + '/' + id.toString());
  }
}
