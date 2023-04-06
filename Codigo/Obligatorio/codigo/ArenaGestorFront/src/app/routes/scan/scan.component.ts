import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TicketScanTicketDto } from 'src/app/models/Tickets/TicketScanTicketDto';
import { TicketsService } from 'src/app/services/tickets.service';

@Component({
  selector: 'app-scan',
  templateUrl: './scan.component.html'
})
export class ScanComponent implements OnInit {

  ticket: TicketScanTicketDto = new TicketScanTicketDto();

  constructor(private ticketService: TicketsService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  Escanear() {
    if (this.ticket.ticketId.length == 0) {
      this.toastr.error("No puedes dejar el id vacío")
    } else {
      this.ticketService.Scan(this.ticket).subscribe(res => {
        this.ticket = new TicketScanTicketDto();
        this.toastr.success("Ticket escaneado correctamente")
      }, error => {
        if (error.error.errors) {
          this.toastr.error("Formato del ID inválido")
        } else {
          this.toastr.error(error.error)
        }
      })
    }
  }

}
