import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SoloistService } from 'src/app/services/soloist.service';
import { ActivatedRoute } from '@angular/router'
import { GenderService } from 'src/app/services/gender.service';
import { ArtistService } from 'src/app/services/artist.service';
import { GenderResultGenderDto } from 'src/app/models/Genders/GenderResultGenderDto';
import { ArtistResultArtistDto } from 'src/app/models/Artists/ArtistResultArtistDto';
import { SoloistUpdateSoloistDto } from 'src/app/models/Soloists/SoloistUpdateSoloistDto';
import { RolesArtistResultDto } from 'src/app/models/Roles/RolesArtistResultDto';
import { RoleService } from 'src/app/services/role.service';

@Component({
  templateUrl: './soloist-form.component.html'
})
export class SoloistUpdateComponent implements OnInit {

  mode: String = "Editar"
  model: SoloistUpdateSoloistDto = new SoloistUpdateSoloistDto()

  genderList: Array<GenderResultGenderDto> = new Array()
  artistList: Array<ArtistResultArtistDto> = new Array()
  rolesArtistList: Array<RolesArtistResultDto> = new Array()

  constructor(private roleService: RoleService, private genderService: GenderService, private artistService: ArtistService, private toastr: ToastrService, private service: SoloistService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.service.GetById(params["id"]).subscribe(soloist => {
        this.model.musicalProtagonistId = soloist.musicalProtagonistId
        this.model.genderId = soloist.gender.genderId
        this.model.name = soloist.name
        this.model.startDate = new Date(soloist.startDate)
        this.model.artistId = soloist.artist.artistId
        this.model.roleArtistId = soloist.roleArtist.roleArtistId
      })
    })
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
    this.service.Update(this.model).subscribe(res => {
      this.toastr.success("Solista actualizado correctamente", "Éxito")
      this.router.navigate(["/administracion/solistas"])
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }

}
