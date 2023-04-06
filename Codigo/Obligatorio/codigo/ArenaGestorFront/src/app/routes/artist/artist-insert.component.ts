import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ArtistService } from 'src/app/services/artist.service';
import { ToastrService } from 'ngx-toastr';
import { ArtistInsertArtistDto } from 'src/app/models/Artists/ArtistInsertArtistDto';
import { UserService } from 'src/app/services/user.service';
import { UserResultUserDto } from 'src/app/models/Users/UserResultUserDto';

@Component({
  templateUrl: './artist-form.component.html'
})
export class ArtistInsertComponent implements OnInit {

  mode: String = "Insertar";
  model: ArtistInsertArtistDto = new ArtistInsertArtistDto()
  usersList: Array<UserResultUserDto> = new Array()

  constructor(private userService: UserService, private toastr: ToastrService, private service: ArtistService, private router: Router) { }

  ngOnInit(): void {
    this.userService.Get(undefined).subscribe(res => {
      this.usersList = res
    })
  }

  Confirmar() {
    this.service.Insert(this.model).subscribe(res => {
      this.toastr.success("Artista agregado correctamente", "Éxito")
      this.router.navigate(["/administracion/artistas"])
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }
}
