import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BandResultBandDto } from 'src/app/models/Bands/BandResultBandDto';
import { ConcertInsertConcertDto } from 'src/app/models/Concerts/ConcertInsertConcertDto';
import { CountryResultDto } from 'src/app/models/Countrys/CountryResultDto';
import { SoloistResultSoloistDto } from 'src/app/models/Soloists/SoloistResultSoloistDto';
import { ConcertService } from 'src/app/services/concert.service';
import { CountryService } from 'src/app/services/country.service';
import { SoloistService } from 'src/app/services/soloist.service';
import { BandService } from 'src/app/services/band.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { ConcertInsertProtagonistDto } from 'src/app/models/Concerts/ConcertInsertProtagonistDto';

@Component({
  templateUrl: './concert-form.component.html'
})
export class ConcertInsertComponent implements OnInit {

  mode: String = "Insertar";
  model: ConcertInsertConcertDto = new ConcertInsertConcertDto();
  countryList: Array<CountryResultDto> = new Array()
  bandList: Array<BandResultBandDto> = new Array()
  soloistList: Array<SoloistResultSoloistDto> = new Array()

  dropdownSettingsBand: IDropdownSettings = {};
  dropdownSettingsSoloist: IDropdownSettings = {};
  selectedBandList: Array<BandResultBandDto> = new Array()
  selectedSoloistList: Array<SoloistResultSoloistDto> = new Array()

  constructor(private soloistService: SoloistService, private bandService: BandService, private countryService: CountryService, private toastr: ToastrService, private service: ConcertService, private router: Router) { }

  ngOnInit(): void {

    this.dropdownSettingsBand = {
      idField: 'musicalProtagonistId',
      textField: 'name',
      enableCheckAll: false,
    };

    this.dropdownSettingsSoloist = {
      idField: 'musicalProtagonistId',
      textField: 'name',
      enableCheckAll: false,
    };

    this.countryService.Get().subscribe(res => {
      this.countryList = res
      this.bandService.Get(undefined).subscribe(res => {
        this.bandList = res
        this.soloistService.Get(undefined).subscribe(res => {
          this.soloistList = res
          this.CargarFullList();
        })
      })
    })
  }

  CargarFullList() {
    this.selectedBandList = new Array();
    this.selectedSoloistList = new Array();

    this.bandList.forEach(band => {
      this.model.protagonists.forEach(protagonist => {
        if (band.musicalProtagonistId == protagonist.musicalProtagonistId) {
          var newBand = new BandResultBandDto();
          newBand.musicalProtagonistId = band.musicalProtagonistId
          newBand.name = band.name
          this.selectedBandList.push(newBand);
        }
      });
    });

    this.soloistList.forEach(soloist => {
      this.model.protagonists.forEach(protagonist => {
        if (soloist.musicalProtagonistId == protagonist.musicalProtagonistId) {
          var newSoloist = new SoloistResultSoloistDto();
          newSoloist.musicalProtagonistId = soloist.musicalProtagonistId
          newSoloist.name = soloist.name
          this.selectedSoloistList.push(newSoloist);
        }
      });
    });
  }

  Confirmar() {
    this.model.protagonists = new Array();
    this.selectedBandList.forEach(selectedBand => {
      var insert = new ConcertInsertProtagonistDto();
      insert.musicalProtagonistId = selectedBand.musicalProtagonistId;
      this.model.protagonists.push(insert);
    });
    this.selectedSoloistList.forEach(selectedSoloist => {
      var insert = new ConcertInsertProtagonistDto();
      insert.musicalProtagonistId = selectedSoloist.musicalProtagonistId;
      this.model.protagonists.push(insert);
    });

    this.service.Insert(this.model).subscribe(res => {
      this.toastr.success("Concierto agregado correctamente", "Éxito")
      this.router.navigate(["/administracion/conciertos"])
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }

}
