import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GenderService } from 'src/app/services/gender.service';
import { ActivatedRoute } from '@angular/router'
import { GenderUpdateGenderDto } from 'src/app/models/Genders/GenderUpdateGenderDto';
import { GenderResultGenderDto } from 'src/app/models/Genders/GenderResultGenderDto';

@Component({
  templateUrl: './gender-form.component.html'
})
export class GenderUpdateComponent implements OnInit {

  mode: String = "Editar"
  model: GenderUpdateGenderDto = new GenderUpdateGenderDto();
  selectedGender: GenderResultGenderDto = new GenderResultGenderDto();

  constructor(private toastr: ToastrService, private service: GenderService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.service.GetById(params["id"]).subscribe(gender => {
        this.model.genderId = gender.genderId
        this.model.name = gender.name
      })
    })
  }

  Confirmar() {
    this.service.Update(this.model).subscribe(res => {
      this.toastr.success("Genero actualizado correctamente", "Éxito")
      this.router.navigate(["/administracion/generos"])
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }

}
