import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BandService } from 'src/app/services/band.service';
import { ToastrService } from 'ngx-toastr';
import { BandGetBandsDto } from 'src/app/models/Bands/BandGetBandsDto';
import { BandResultBandDto } from 'src/app/models/Bands/BandResultBandDto';

@Component({
  selector: 'app-protagonist-bands',
  templateUrl: './protagonist.bands.component.html'
})
export class ProtagonistBandsComponent implements OnInit {

  bandList: Array<BandResultBandDto> = new Array()
  filter: String = "";

  constructor(private toastr: ToastrService, private service: BandService, private router: Router) { }

  ngOnInit(): void {
    this.GetData()
  }

  GetData() {
    let filterDto: BandGetBandsDto = { name: this.filter };

    this.service.Get(filterDto).subscribe(res => {
      this.bandList = res
    })
  }

}
