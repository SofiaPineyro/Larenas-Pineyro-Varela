import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BandService } from 'src/app/services/band.service';
import { BandResultBandDto } from 'src/app/models/Bands/BandResultBandDto';

@Component({
  selector: 'app-protagonist-band',
  templateUrl: './protagonist.band.component.html'
})
export class ProtagonistBandComponent implements OnInit {

  model: BandResultBandDto = new BandResultBandDto()

  constructor(private service: BandService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.GetData()
  }

  GetData(): void {
    this.activatedRoute.params.subscribe(params => {
      this.service.GetById(params["id"]).subscribe(result => {
        this.model = result;
      })
    })
  }

}
