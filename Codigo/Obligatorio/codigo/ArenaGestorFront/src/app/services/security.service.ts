import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Events } from '../app.events';
import { SecurityLoggedUser } from '../models/Security/SecurityLoggedUser';
import { SecurityLoginDto } from '../models/Security/SecurityLoginDto';
import { SecurityTokenResponseDto } from '../models/Security/SecurityTokenResponseDto';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {

  private loggedUser?: SecurityLoggedUser;
  private apiUrl: string

  constructor(private http: HttpClient, private cookies: CookieService, private events: Events, private router: Router) {
    this.apiUrl = environment.apiURL + "security"
  }

  Login(securityLogin: SecurityLoginDto) {
    return this.http.post<SecurityTokenResponseDto>(this.apiUrl + "/login", securityLogin)
      .pipe(map((x: SecurityTokenResponseDto) => {
        localStorage.setItem('ArenaGestorToken', x.token.toString())
        this.GetLoggedUser().subscribe(_ => {
          this.events.LoggedStateChanged.emit(true)
        })
        return x;
      }))
  }

  Logout() {
    return this.http.post(this.apiUrl + "/logout", {}).subscribe(res => {
      localStorage.removeItem('ArenaGestorToken');
      this.loggedUser = undefined
      this.events.LoggedStateChanged.emit(false)
      this.router.navigate(["/"])
    })
  }

  IsLogged() {
    return this.loggedUser != null
  }

  isAuthorizedByRoles(Roles: Array<String>): boolean {
    if (!this.IsLogged()) {
      return false;
    }
    let authorized = false;
    for (let i = 0; i < this.loggedUser!.roles.length; i++) {
      const element = this.loggedUser!.roles[i];
      for (let j = 0; j < Roles.length; j++) {
        const roleElement = Roles[j];
        if (roleElement == element.name) {
          authorized = true;
        }
      }
    }
    return authorized;
  }

  isAuthorized(RoleName: String): boolean {
    if (!this.IsLogged()) {
      return false;
    }
    let authorized = false;
    for (let i = 0; i < this.loggedUser!.roles.length; i++) {
      const element = this.loggedUser!.roles[i];
      if (element.name == RoleName) {
        authorized = true;
      }
    }
    return authorized;
  }

  GetLoggedUser(): Observable<SecurityLoggedUser> {
    let token = localStorage.getItem('ArenaGestorToken')
    if (token) {
      if (this.loggedUser) {
        return new Observable(observer => {
          observer.next(this.loggedUser)
        })
      } else {
        return this.http.get<SecurityLoggedUser>(this.apiUrl + "/user").pipe(
          map(user => {
            this.loggedUser = user
            this.events.LoggedStateChanged.emit(true)
            return user
          })
        )
      }
    } else {
      return new Observable(observer => {
        observer.next(undefined)
      })
    }
  }

}
