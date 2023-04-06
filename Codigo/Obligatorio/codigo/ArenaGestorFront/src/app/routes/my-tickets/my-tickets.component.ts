import { Component, OnInit } from '@angular/core';
import { TicketGetTicketResultDto } from 'src/app/models/Tickets/TicketGetTicketResultDto';
import { TicketsService } from 'src/app/services/tickets.service';

@Component({
  selector: 'app-my-tickets',
  templateUrl: './my-tickets.component.html'
})
export class MyTicketsComponent implements OnInit {

  ticketList: Array<TicketGetTicketResultDto> = new Array();

  constructor(private ticketService: TicketsService) { }

  ngOnInit(): void {
    this.ticketService.GetOfLoggedUser().subscribe(res => {
      this.ticketList = res;
    })
  }

}
