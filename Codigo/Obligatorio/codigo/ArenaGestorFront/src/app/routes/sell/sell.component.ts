import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ConcertGetConcertsDto } from 'src/app/models/Concerts/ConcertGetConcertsDto';
import { ConcertResultConcertDto } from 'src/app/models/Concerts/ConcertResultConcertDto';
import { TicketSellTicketDto } from 'src/app/models/Tickets/TicketSellTicketDto';
import { ConcertService } from 'src/app/services/concert.service';
import { TicketsService } from 'src/app/services/tickets.service';

@Component({
  selector: 'app-sell',
  templateUrl: './sell.component.html'
})
export class SellComponent implements OnInit {

  concertList: Array<ConcertResultConcertDto> = new Array()
  selectedConcert: TicketSellTicketDto = new TicketSellTicketDto();

  email: String = "";
  amount: Number = 0;
  selectedTourName: String = "";

  constructor(private concertService: ConcertService, private ticketService: TicketsService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.concertService.GetUpcoming().subscribe(res => {
      this.concertList = res;
    })
  }

  Vender(concert: ConcertResultConcertDto) {
    this.selectedConcert.concertId = concert.concertId;
    this.selectedTourName = concert.tourName;
    this.email = "";
    this.amount = 0;
  }

  Confirmar() {
    this.selectedConcert.email = this.email;
    this.selectedConcert.amount = this.amount;

    this.ticketService.Sale(this.selectedConcert).subscribe(res => {
      this.toastr.success("Ticket vendido con ID: " + res.ticketId)
    }, error => {
      this.toastr.error(error.error)
    })
  }

}
