import { Component, Input, OnInit } from '@angular/core';
import { SecurityLoggedUser } from 'src/app/models/Security/SecurityLoggedUser';
import { SecurityService } from 'src/app/services/security.service';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {

  @Input() User?: SecurityLoggedUser

  isEspectador: Boolean = false;
  isArtista: Boolean = false;

  constructor(private security: SecurityService) { }

  ngOnInit(): void {
    this.isEspectador = this.security.isAuthorized("Espectador")
    this.isArtista = this.security.isAuthorized("Artista")
  }

  Logout() {
    this.security.Logout()
  }

}
