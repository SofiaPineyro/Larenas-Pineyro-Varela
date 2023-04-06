import { Component, OnInit } from '@angular/core';
import { ArtistService } from 'src/app/services/artist.service';
import { GenderService } from 'src/app/services/gender.service';
import { SoloistService } from 'src/app/services/soloist.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { GenderResultGenderDto } from 'src/app/models/Genders/GenderResultGenderDto';
import { ArtistResultArtistDto } from 'src/app/models/Artists/ArtistResultArtistDto';
import { SoloistInsertSoloistDto } from 'src/app/models/Soloists/SoloistInsertSoloistDto';
import { RolesArtistResultDto } from 'src/app/models/Roles/RolesArtistResultDto';
import { RoleService } from 'src/app/services/role.service';

@Component({
  templateUrl: './soloist-form.component.html'
})
export class SoloistInsertComponent implements OnInit {

  mode: String = "Insertar";
  model: SoloistInsertSoloistDto = new SoloistInsertSoloistDto()

  genderList: Array<GenderResultGenderDto> = new Array()
  artistList: Array<ArtistResultArtistDto> = new Array()
  rolesArtistList: Array<RolesArtistResultDto> = new Array()

  constructor(private roleService: RoleService, private genderService: GenderService, private artistService: ArtistService, private service: SoloistService, private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.genderService.Get(undefined).subscribe(res => {
      this.genderList = res
    })
    this.artistService.Get(undefined).subscribe(res => {
      this.artistList = res;
    })
    this.roleService.GetArtistRoles().subscribe(res => {
      this.rolesArtistList = res;
    })
  }

  Confirmar() {
    this.service.Insert(this.model).subscribe(res => {
      this.toastr.success("Solista agregado correctamente", "Éxito")
      this.router.navigate(["/administracion/solistas"])
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }

}
