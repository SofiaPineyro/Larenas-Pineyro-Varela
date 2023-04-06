import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConcertGetConcertsDto } from 'src/app/models/Concerts/ConcertGetConcertsDto';
import { ConcertResultConcertDto } from 'src/app/models/Concerts/ConcertResultConcertDto';
import { ConcertService } from 'src/app/services/concert.service';

@Component({
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {

  concertsList: Array<ConcertResultConcertDto> = new Array<ConcertResultConcertDto>()
  filter: ConcertGetConcertsDto = new ConcertGetConcertsDto()
  concertToView: Number = 0;

  constructor(private toastr: ToastrService, private service: ConcertService, private router: Router) { }

  ngOnInit(): void {
    this.filter.upcoming = true;
    this.filter.dateRange.startDate = new Date(this.filter.dateRange.endDate.setFullYear(2021));
    this.filter.dateRange.endDate = new Date(this.filter.dateRange.endDate.setFullYear(2023));
    this.GetUpcoming()
  }

  GetData() {
    this.service.Get(this.filter).subscribe(res => {
      this.concertsList = res
    })
  }

  GetUpcoming() {
    this.service.GetUpcoming().subscribe(res => {
      this.concertsList = res
    })
  }

}
