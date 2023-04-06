import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BandService } from 'src/app/services/band.service';
import { ToastrService } from 'ngx-toastr';
import { BandGetBandsDto } from 'src/app/models/Bands/BandGetBandsDto';
import { BandResultBandDto } from 'src/app/models/Bands/BandResultBandDto';

@Component({
  templateUrl: './band.component.html'
})
export class BandComponent implements OnInit {

  bandList: Array<BandResultBandDto> = new Array()
  bandToDelete: Number = 0;
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

  SetBandToDelete(id: Number) {
    this.bandToDelete = id;
  }

  Delete() {
    this.service.Delete(this.bandToDelete).subscribe(res => {
      this.toastr.success("Banda borrada correctamente", "Éxito")
      this.GetData();
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }

}
