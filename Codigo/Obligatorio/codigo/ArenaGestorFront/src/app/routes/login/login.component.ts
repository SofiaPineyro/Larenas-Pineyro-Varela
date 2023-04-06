import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Events } from 'src/app/app.events';
import { SecurityLoginDto } from 'src/app/models/Security/SecurityLoginDto';
import { SecurityService } from 'src/app/services/security.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  user: SecurityLoginDto = new SecurityLoginDto();
  error: String = "";

  constructor(private security: SecurityService, private events: Events, private cookies: CookieService, private router: Router) { }

  ngOnInit(): void {
    if (this.security.IsLogged()) {
      this.router.navigate(["/"])
    }
  }

  Login() {
    this.security.Login(this.user).subscribe(
      (response: any) => {
        this.error = ""
        this.router.navigate(['/']);
      },
      err => {
        this.error = ""
        if (err.status == 400) {
          for (const [key, value] of Object.entries(err.error.errors)) {
            let values = Object(value)
            for (let i = 0; i < values.length; i++) {
              this.error = this.error + values[i] + "<br>";
            }
          }
        } else {
          this.error = err.error
        }
      }
    )
  }

}
