import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SoloistService } from 'src/app/services/soloist.service';
import { ToastrService } from 'ngx-toastr';
import { SoloistGetSoloistsDto } from 'src/app/models/Soloists/SoloistGetSoloistsDto';
import { SoloistResultSoloistDto } from 'src/app/models/Soloists/SoloistResultSoloistDto';

@Component({
  selector: 'app-soloist',
  templateUrl: './soloist.component.html'
})
export class SoloistComponent implements OnInit {

  soloistList: Array<SoloistResultSoloistDto> = new Array()
  filter: String = "";

  soloistToDelete: Number = 0;

  constructor(private toastr: ToastrService, private service: SoloistService, private router: Router) { }

  ngOnInit(): void {
    this.GetData()
  }

  GetData() {
    let filterDto: SoloistGetSoloistsDto = { name: this.filter };
    this.service.Get(filterDto).subscribe(res => {
      this.soloistList = res
    })
  }

  SetSoloistToDelete(id: Number) {
    this.soloistToDelete = id;
  }

  Delete() {
    this.service.Delete(this.soloistToDelete).subscribe(res => {
      this.toastr.success("Solista eliminado correctamente", "Éxito")
      this.GetData();
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }
}
