import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ArtistService } from 'src/app/services/artist.service';
import { ActivatedRoute } from '@angular/router'
import { ArtistUpdateArtistDto } from 'src/app/models/Artists/ArtistUpdateArtistDto';
import { UserResultUserDto } from 'src/app/models/Users/UserResultUserDto';
import { UserService } from 'src/app/services/user.service';

@Component({
  templateUrl: './artist-form.component.html'
})
export class ArtistUpdateComponent implements OnInit {

  mode: String = "Editar"
  model: ArtistUpdateArtistDto = new ArtistUpdateArtistDto();
  usersList: Array<UserResultUserDto> = new Array()

  constructor(private userService: UserService, private toastr: ToastrService, private service: ArtistService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.service.GetById(params["id"]).subscribe(artist => {
        this.model.artistId = artist.artistId
        this.model.name = artist.name
        this.model.userId = artist.userId
        this.userService.Get(undefined).subscribe(res => {
          this.usersList = res
        })
      })
    })
  }

  Confirmar() {
    this.service.Update(this.model).subscribe(res => {
      this.toastr.success("Artista actualizado correctamente", "Éxito")
      this.router.navigate(["/administracion/artistas"])
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }

}
