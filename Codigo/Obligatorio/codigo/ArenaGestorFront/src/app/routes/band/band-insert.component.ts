import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { ToastrService } from 'ngx-toastr';
import { ArtistResultArtistDto } from 'src/app/models/Artists/ArtistResultArtistDto';
import { BandArtistRole } from 'src/app/models/Bands/BandArtistRole';
import { BandInsertArtistDto } from 'src/app/models/Bands/BandInsertArtistDto';
import { BandInsertBandDto } from 'src/app/models/Bands/BandInsertBandDto';
import { GenderResultGenderDto } from 'src/app/models/Genders/GenderResultGenderDto';
import { RolesArtistResultDto } from 'src/app/models/Roles/RolesArtistResultDto';
import { ArtistService } from 'src/app/services/artist.service';
import { BandService } from 'src/app/services/band.service';
import { GenderService } from 'src/app/services/gender.service';
import { RoleService } from 'src/app/services/role.service';

@Component({
  templateUrl: './band-form.component.html'
})
export class BandInsertComponent implements OnInit {

  mode: String = "Insertar";
  model: BandInsertBandDto = new BandInsertBandDto()

  genderList: Array<GenderResultGenderDto> = new Array()
  artistList: Array<ArtistResultArtistDto> = new Array()
  rolesArtistList: Array<RolesArtistResultDto> = new Array()

  fullRolesArtistList: Array<BandArtistRole> = new Array()
  selectedRolesArtistList: Array<BandArtistRole> = new Array()

  dropdownSettings: IDropdownSettings = {};

  constructor(private roleService: RoleService, private genderService: GenderService, private artistService: ArtistService, private service: BandService, private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.CargarConfiguracion();
    this.CargarSelects();
  }

  CargarConfiguracion(): void {
    this.dropdownSettings = {
      idField: 'key',
      textField: 'name',
      enableCheckAll: false,
    };
  }

  CargarSelects(): void {
    this.genderService.Get(undefined).subscribe(res => {
      this.genderList = res;
      this.artistService.Get(undefined).subscribe(res => {
        this.artistList = res;
        this.roleService.GetArtistRoles().subscribe(res => {
          this.rolesArtistList = res;
        });
      });
    });
  }

  AgregarArtista(){
    this.model.artists.push(new BandInsertArtistDto())
  }

  BorrarArtista(ArtistId : Number){
    this.model.artists = this.model.artists.filter(obj => {return obj.artistId !== ArtistId});
  }

  Confirmar() {
    this.service.Insert(this.model).subscribe(res => {
      this.toastr.success("Banda agregada correctamente", "Éxito")
      this.router.navigate(["/administracion/bandas"])
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }
}
