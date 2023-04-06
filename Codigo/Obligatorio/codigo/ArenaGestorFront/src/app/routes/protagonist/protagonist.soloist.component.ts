import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SoloistService } from 'src/app/services/soloist.service';
import { SoloistResultSoloistDto } from 'src/app/models/Soloists/SoloistResultSoloistDto';

@Component({
  selector: 'app-protagonist-soloist',
  templateUrl: './protagonist.soloist.component.html'
})
export class ProtagonistSoloistComponent implements OnInit {

  model: SoloistResultSoloistDto = new SoloistResultSoloistDto()

  constructor(private service: SoloistService, private router: Router, private activatedRoute: ActivatedRoute) { }

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
