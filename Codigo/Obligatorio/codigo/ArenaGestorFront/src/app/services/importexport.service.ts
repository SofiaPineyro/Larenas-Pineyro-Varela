import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ImportResult } from '../models/ImportResult';

@Injectable({
  providedIn: 'root'
})
export class ImportexportService {

  private apiUrl: string

  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiURL + "importexport"
  }

  GetMethods() {
    return this.http.get<Array<string>>(this.apiUrl)
  }

  Export(path: string, method: string) {
    let request = { path: path, method: method }
    return this.http.post<string>(this.apiUrl + "/export", request)
  }

  Import(path: string, method: string) {
    let request = { path: path, method: method }
    return this.http.post<ImportResult>(this.apiUrl + "/import", request)
  }
}
