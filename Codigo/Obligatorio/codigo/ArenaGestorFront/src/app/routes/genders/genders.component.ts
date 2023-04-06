import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GenderGetGendersDto } from 'src/app/models/Genders/GenderGetGendersDto';
import { GenderResultGenderDto } from 'src/app/models/Genders/GenderResultGenderDto';
import { GenderService } from 'src/app/services/gender.service';

@Component({
  templateUrl: './genders.component.html'
})
export class GendersComponent implements OnInit {

  genderList: Array<GenderResultGenderDto> = new Array<GenderResultGenderDto>()
  filter: String = "";

  constructor(private service: GenderService, private router: Router) { }

  ngOnInit(): void {
    this.GetData()
  }

  GetData() {
    let filterDto: GenderGetGendersDto = { name: this.filter };
    this.service.Get(filterDto).subscribe(res => {
      this.genderList = res
    })
  }
}
