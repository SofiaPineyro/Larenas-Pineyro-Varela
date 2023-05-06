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

  Buy(
    snacks: Array<{ id: Number; quantity: Number }>
  ): Observable<Array<SnackResultDto>> {
    const payload = JSON.stringify(snacks);
    return this.http.post<Array<SnackResultDto>>(this.apiUrl, payload);
  }

  Insert(snack: SnackInsertDto): Observable<SnackResultDto> {
    return this.http.post<SnackResultDto>(this.apiUrl, snack);
  }

  Delete(id: Number) {
    return this.http.delete(this.apiUrl + '/' + id.toString());
  }
}
