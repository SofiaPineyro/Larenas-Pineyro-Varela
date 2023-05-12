import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SnackResultDto } from 'src/app/models/Snacks/SnackResultDto';
import { TicketBuyTicketDto } from 'src/app/models/Tickets/TicketBuyTicketDto';
import { ConcertService } from 'src/app/services/concert.service';
import { SnacksService } from 'src/app/services/snacks.service';
import { TicketsService } from 'src/app/services/tickets.service';

@Component({
  selector: 'app-buy',
  templateUrl: './buy.component.html',
})
export class BuyComponent implements OnInit {
  selectedTourName: String = '';
  selectedId: Number = 0;
  amount: Number = 0;
  concertPrice: Number = 0;
  snackList: Array<{
    key: String;
    value: { price: Number; quantity: Number };
  }> = [];
  snacksMap = new Map<String, { price: Number; quantity: Number }>();
  totalPrice: Number = 0;

  constructor(
    private toastr: ToastrService,
    private ticketService: TicketsService,
    private snacksService: SnacksService,
    private service: ConcertService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      this.service.GetById(params['id']).subscribe((concert) => {
        this.selectedTourName = concert.tourName;
        this.selectedId = concert.concertId;
        this.concertPrice = concert.price;
      });
    });
  }

  onChange(event: any) {
    this.totalPrice = event.target.value * this.concertPrice.valueOf();
  }

  onSnackAdded(snack: SnackResultDto) {
    this.totalPrice = this.totalPrice.valueOf() + snack.price.valueOf();
    const snackKey = `${snack.id}-${snack.name}`;
    const existingSnack = this.snacksMap.get(snackKey);

    if (existingSnack) {
      this.snacksMap.set(snackKey, {
        price: existingSnack.price.valueOf() + snack.price.valueOf(),
        quantity: existingSnack.quantity.valueOf() + 1,
      });
    } else {
      this.snacksMap.set(snackKey, { price: snack.price, quantity: 1 });
    }

    this.snackList = Array.from(this.snacksMap, ([key, value]) => ({
      key,
      value,
    }));
  }

  Confirmar() {
    let dto = new TicketBuyTicketDto();
    dto.Amount = this.amount;
    dto.concertId = this.selectedId;
    this.ticketService.Shopping(dto).subscribe(
      (res) => {
        const snacks: Array<{ id: Number; quantity: Number }> = [];
        this.snacksMap.forEach((value, key) => {
          snacks.push({
            id: parseInt(key.split('-')[0]),
            quantity: value.quantity,
          });
        });
        snacks.forEach((snack) => {
          this.snacksService.Buy(snack).subscribe(
            (res2) => {
              this.toastr.success('Ticket comprado con ID: ' + res.ticketId);
            },
            (error) => {
              this.toastr.error(
                'Se han comprado los tickets, pero la compra de snacks ha fallado con error' +
                  error.error
              );
            }
          );
        });
      },
      (error) => {
        this.toastr.error(error.error);
      }
    );
  }
}
