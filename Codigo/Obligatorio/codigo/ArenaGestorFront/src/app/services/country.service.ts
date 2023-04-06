import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { CountryResultDto } from '../models/Countrys/CountryResultDto';

@Injectable({
    providedIn: 'root'
})
export class CountryService {

    private apiUrl: string

    constructor(private http: HttpClient) {
        this.apiUrl = environment.apiURL + "countrys"
    }

    Get(): Observable<Array<CountryResultDto>> {
        return this.http.get<Array<CountryResultDto>>(this.apiUrl)
    }

}
