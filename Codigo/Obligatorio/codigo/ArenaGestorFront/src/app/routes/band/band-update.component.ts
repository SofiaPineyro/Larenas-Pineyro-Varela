import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { ToastrService } from 'ngx-toastr';
import { ArtistResultArtistDto } from 'src/app/models/Artists/ArtistResultArtistDto';
import { BandArtistRole } from 'src/app/models/Bands/BandArtistRole';
import { BandUpdateArtistDto } from 'src/app/models/Bands/BandUpdateArtistDto';
import { BandUpdateBandDto } from 'src/app/models/Bands/BandUpdateBandDto';
import { GenderResultGenderDto } from 'src/app/models/Genders/GenderResultGenderDto';
import { RolesArtistResultDto } from 'src/app/models/Roles/RolesArtistResultDto';
import { ArtistService } from 'src/app/services/artist.service';
import { BandService } from 'src/app/services/band.service';
import { GenderService } from 'src/app/services/gender.service';
import { RoleService } from 'src/app/services/role.service';

@Component({
  templateUrl: './band-form.component.html'
})
export class BandUpdateComponent implements OnInit {

  mode: String = "Actualizar";
  model: BandUpdateBandDto = new BandUpdateBandDto()

  genderList: Array<GenderResultGenderDto> = new Array()
  artistList: Array<ArtistResultArtistDto> = new Array()
  rolesArtistList: Array<RolesArtistResultDto> = new Array()

  fullRolesArtistList: Array<BandArtistRole> = new Array()
  selectedRolesArtistList: Array<BandArtistRole> = new Array()

  dropdownSettings: IDropdownSettings = {};

  constructor(private roleService: RoleService, private genderService: GenderService, private artistService: ArtistService, private service: BandService, private toastr: ToastrService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.CargarConfiguracion();
    this.CargarActualizar();
    this.CargarSelects();
  }

  CargarConfiguracion(): void {
    this.dropdownSettings = {
      idField: 'key',
      textField: 'name',
      enableCheckAll: false,
    };
  }

  CargarActualizar(): void {
    this.activatedRoute.params.subscribe(params => {
      this.service.GetById(params["id"]).subscribe(band => {
        this.model.genderId = band.gender.genderId
        this.model.musicalProtagonistId = band.musicalProtagonistId
        this.model.name = band.name
        this.model.startDate = new Date(band.startDate)
        this.model.artists = new Array()
        band.artists.forEach(artist => {
          let artistUpdate = new BandUpdateArtistDto();
          artistUpdate.artistId = artist.artistId
          artistUpdate.roleArtistId = artist.roleArtist.roleArtistId;
          this.model.artists.push(artistUpdate);
        });
      })
    })
  }

  AgregarArtista(){
    this.model.artists.push(new BandUpdateArtistDto())
  }

  BorrarArtista(ArtistId : Number){
    this.model.artists = this.model.artists.filter(obj => {return obj.artistId !== ArtistId});
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

  Confirmar() {
    this.service.Update(this.model).subscribe(res => {
      this.toastr.success("Banda actualizada correctamente", "Éxito")
      this.router.navigate(["/administracion/bandas"])
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }
}
