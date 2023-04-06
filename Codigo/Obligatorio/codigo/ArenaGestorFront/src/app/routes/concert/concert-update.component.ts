import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router'
import { ConcertUpdateConcertDto } from 'src/app/models/Concerts/ConcertUpdateConcertDto';
import { ConcertService } from 'src/app/services/concert.service';
import { CountryResultDto } from 'src/app/models/Countrys/CountryResultDto';
import { CountryService } from 'src/app/services/country.service';
import { SoloistService } from 'src/app/services/soloist.service';
import { BandService } from 'src/app/services/band.service';
import { BandResultBandDto } from 'src/app/models/Bands/BandResultBandDto';
import { SoloistResultSoloistDto } from 'src/app/models/Soloists/SoloistResultSoloistDto';
import { ConcertUpdateProtagonistDto } from 'src/app/models/Concerts/ConcertUpdateProtagonistDto';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { ConcertUpdateLocationDto } from 'src/app/models/Concerts/ConcertUpdateLocationDto';
import { ConcertUpdateCountryDto } from 'src/app/models/Concerts/ConcertUpdateCountryDto';

@Component({
  templateUrl: './concert-form.component.html'
})
export class ConcertUpdateComponent implements OnInit {

  mode: String = "Actualizar"
  model: ConcertUpdateConcertDto = new ConcertUpdateConcertDto();
  countryList: Array<CountryResultDto> = new Array()
  bandList: Array<BandResultBandDto> = new Array()
  soloistList: Array<SoloistResultSoloistDto> = new Array()

  dropdownSettingsBand: IDropdownSettings = {};
  dropdownSettingsSoloist: IDropdownSettings = {};
  selectedBandList: Array<BandResultBandDto> = new Array()
  selectedSoloistList: Array<SoloistResultSoloistDto> = new Array()

  constructor(private soloistService: SoloistService, private bandService: BandService, private countryService: CountryService, private toastr: ToastrService, private service: ConcertService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.service.GetById(params["id"]).subscribe(concert => {
        this.model.concertId = concert.concertId
        this.model.tourName = concert.tourName
        this.model.date = new Date(concert.date)
        this.model.ticketCount = concert.ticketCount
        this.model.price = concert.price;

        this.model.location = new ConcertUpdateLocationDto();
        this.model.location.locationId = concert.location.locationId
        this.model.location.number = concert.location.number
        this.model.location.place = concert.location.place
        this.model.location.street = concert.location.street
        this.model.location.countryId = concert.location.country.countryId

        concert.protagonists.forEach(element => {
          let protagonist = new ConcertUpdateProtagonistDto();
          protagonist.musicalProtagonistId = element.musicalProtagonistId
          this.model.protagonists.push(protagonist)
        });
      })
    })

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
      var insert = new ConcertUpdateProtagonistDto();
      insert.musicalProtagonistId = selectedBand.musicalProtagonistId;
      this.model.protagonists.push(insert);
    });
    this.selectedSoloistList.forEach(selectedSoloist => {
      var insert = new ConcertUpdateProtagonistDto();
      insert.musicalProtagonistId = selectedSoloist.musicalProtagonistId;
      this.model.protagonists.push(insert);
    });

    this.service.Update(this.model).subscribe(res => {
      this.toastr.success("Concierto actualizado correctamente", "Éxito")
      this.router.navigate(["/administracion/conciertos"])
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }

}
