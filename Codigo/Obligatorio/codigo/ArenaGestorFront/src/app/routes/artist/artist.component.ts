import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ArtistService } from 'src/app/services/artist.service';
import { ToastrService } from 'ngx-toastr';
import { ArtistResultArtistDto } from 'src/app/models/Artists/ArtistResultArtistDto';
import { ArtistGetArtistsDto } from 'src/app/models/Artists/ArtistGetArtistsDto';

@Component({
  templateUrl: './artist.component.html'
})
export class ArtistComponent implements OnInit {

  artistList: Array<ArtistResultArtistDto> = new Array<ArtistResultArtistDto>()
  filter: String = "";
  artistToDelete: Number = 0;

  constructor(private toastr: ToastrService, private service: ArtistService, private router: Router) { }

  ngOnInit(): void {
    this.GetData()
  }

  GetData() {
    let filterDto: ArtistGetArtistsDto = { name: this.filter };
    this.service.Get(filterDto).subscribe(res => {
      this.artistList = res
    })
  }

  SetArtistToDelete(id: Number) {
    this.artistToDelete = id;
  }

  Delete() {
    this.service.Delete(this.artistToDelete).subscribe(res => {
      this.toastr.success("Artista eliminado correctamente", "Éxito")
      this.GetData();
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }
}
