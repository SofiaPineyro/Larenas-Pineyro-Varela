import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SoloistService } from 'src/app/services/soloist.service';
import { ToastrService } from 'ngx-toastr';
import { SoloistGetSoloistsDto } from 'src/app/models/Soloists/SoloistGetSoloistsDto';
import { SoloistResultSoloistDto } from 'src/app/models/Soloists/SoloistResultSoloistDto';

@Component({
  selector: 'app-protagonist-soloists',
  templateUrl: './protagonist.soloists.component.html'
})
export class ProtagonistSoloistsComponent implements OnInit {

  soloistList: Array<SoloistResultSoloistDto> = new Array()
  filter: String = "";

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

}
