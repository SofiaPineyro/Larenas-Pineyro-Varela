import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TicketBuyTicketDto } from 'src/app/models/Tickets/TicketBuyTicketDto';
import { ConcertService } from 'src/app/services/concert.service';
import { TicketsService } from 'src/app/services/tickets.service';

@Component({
  selector: 'app-buy',
  templateUrl: './buy.component.html'
})
export class BuyComponent implements OnInit {

  selectedTourName: String = "";
  selectedId: Number = 0;
  amount : Number = 0;

  constructor(private toastr: ToastrService, private ticketService: TicketsService, private service: ConcertService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.service.GetById(params["id"]).subscribe(concert => { 
        this.selectedTourName = concert.tourName
        this.selectedId = concert.concertId
      })
    })
  }

  Confirmar() {
    let dto = new TicketBuyTicketDto()
    dto.Amount = this.amount
    dto.concertId = this.selectedId
    this.ticketService.Shopping(dto).subscribe(res => {
      this.toastr.success("Ticket comprado con ID: " + res.ticketId)
    }, error => {
      this.toastr.error(error.error)
    })
  }

}
