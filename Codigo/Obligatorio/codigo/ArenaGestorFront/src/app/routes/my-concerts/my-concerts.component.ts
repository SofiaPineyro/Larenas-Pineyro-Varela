import { Component, OnInit } from '@angular/core';
import { ConcertGetConcertsDto } from 'src/app/models/Concerts/ConcertGetConcertsDto';
import { ConcertResultConcertArtistDto } from 'src/app/models/Concerts/ConcertResultConcertArtistDto';
import { ConcertService } from 'src/app/services/concert.service';

@Component({
  selector: 'app-my-concerts',
  templateUrl: './my-concerts.component.html'
})
export class MyConcertsComponent implements OnInit {

  concertsList: Array<ConcertResultConcertArtistDto> = new Array<ConcertResultConcertArtistDto>()
  filter: ConcertGetConcertsDto = new ConcertGetConcertsDto()

  constructor(private service: ConcertService) { }

  ngOnInit(): void {
    this.filter.upcoming = true;
    this.filter.dateRange.startDate = new Date(this.filter.dateRange.endDate.setFullYear(2021));
    this.filter.dateRange.endDate = new Date(this.filter.dateRange.endDate.setFullYear(2023));
    this.GetData();
  }

  GetData() {
    this.service.GetByArtist(this.filter).subscribe(res => {
      this.concertsList = res
    })
  }

}
