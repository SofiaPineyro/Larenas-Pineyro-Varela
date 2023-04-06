import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GenderInsertGenderDto } from 'src/app/models/Genders/GenderInsertGenderDto';
import { GenderResultGenderDto } from 'src/app/models/Genders/GenderResultGenderDto';
import { GenderService } from 'src/app/services/gender.service';

@Component({
  templateUrl: './gender-form.component.html'
})
export class GenderInsertComponent implements OnInit {

  mode: String = "Insertar";
  model: GenderInsertGenderDto = new GenderInsertGenderDto();

  constructor(private toastr: ToastrService, private service: GenderService, private router: Router) { }

  ngOnInit(): void {
  }

  Confirmar() {
    this.service.Insert(this.model).subscribe(res => {
      this.toastr.success("Genero agregado correctamente", "Éxito")
      this.router.navigate(["/administracion/generos"])
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }

}
