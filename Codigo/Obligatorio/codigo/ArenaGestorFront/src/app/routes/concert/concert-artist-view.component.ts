import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router'
import { ConcertService } from 'src/app/services/concert.service';
import { SoloistResultSoloistDto } from 'src/app/models/Soloists/SoloistResultSoloistDto';
import { SoloistService } from 'src/app/services/soloist.service';
import { BandService } from 'src/app/services/band.service';
import { BandResultBandDto } from 'src/app/models/Bands/BandResultBandDto';
import { ConcertResultConcertArtistDto } from 'src/app/models/Concerts/ConcertResultConcertArtistDto';

@Component({
  templateUrl: './concert-view.component.html'
})
export class ConcertArtistViewComponent implements OnInit {

  mode: String = "Ver"
  id: String = ""
  isSoldOut: boolean = false;
  isArtist: boolean = false;

  model: ConcertResultConcertArtistDto = new ConcertResultConcertArtistDto();
  soloistList: Array<SoloistResultSoloistDto> = new Array()
  bandList: Array<BandResultBandDto> = new Array()

  constructor(private bandService: BandService, private soloistService: SoloistService, private service: ConcertService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.id = params["id"];
      this.service.GetByArtist(undefined).subscribe(concerts => {
        if (!concerts) {
          return;
        }
        concerts.forEach(concert => {
          if (concert.concertId == +this.id) {
            this.model = concert;
          }
        });

        if (!this.model) {
          return;
        }

        this.isSoldOut = this.model.ticketCount <= this.model.ticketSold
        this.isArtist = true

        this.soloistService.Get(undefined).subscribe(soloists => {
          this.bandService.Get(undefined).subscribe(bands => {

            soloists.forEach(soloist => {
              this.model.protagonists.forEach(mySoloists => {
                if (soloist.musicalProtagonistId == mySoloists.musicalProtagonistId) {
                  this.soloistList.push(soloist);
                }
              });
            });

            bands.forEach(band => {
              this.model.protagonists.forEach(myBand => {
                if (band.musicalProtagonistId == myBand.musicalProtagonistId) {
                  this.bandList.push(band);
                }
              });
            });

          })
        })
      })
    })
  }

}
