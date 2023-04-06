import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { TicketGetTicketResultDto } from '../models/Tickets/TicketGetTicketResultDto';
import { TicketScanTicketDto } from '../models/Tickets/TicketScanTicketDto';
import { TicketScanTicketResultDto } from '../models/Tickets/TicketScanTicketResultDto';
import { TicketBuyTicketDto } from '../models/Tickets/TicketBuyTicketDto';
import { TicketBuyTicketResultDto } from '../models/Tickets/TicketBuyTicketResultDto';
import { TicketSellTicketDto } from '../models/Tickets/TicketSellTicketDto';
import { TicketSellTicketResultDto } from '../models/Tickets/TicketSellTicketResultDto';

@Injectable({
  providedIn: 'root'
})
export class TicketsService {

  private apiUrl: string

  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiURL + "tickets"
  }

  GetOfLoggedUser(): Observable<Array<TicketGetTicketResultDto>> {
    return this.http.get<Array<TicketGetTicketResultDto>>(this.apiUrl)
  }

  Scan(ticket: TicketScanTicketDto): Observable<TicketScanTicketResultDto> {
    return this.http.put<TicketScanTicketResultDto>(this.apiUrl, ticket)
  }

  Shopping(ticket: TicketBuyTicketDto): Observable<TicketBuyTicketResultDto> {
    return this.http.post<TicketBuyTicketResultDto>(this.apiUrl + "/Shopping", ticket)
  }

  Sale(ticket: TicketSellTicketDto): Observable<TicketSellTicketResultDto> {
    return this.http.post<TicketSellTicketResultDto>(this.apiUrl + "/Sale", ticket)
  }

}
