import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from "rxjs";
import { Events } from './app.events';
import { SecurityService } from './services/security.service';

@Injectable()
export class AppHttpInterceptor implements HttpInterceptor {
    constructor(private events: Events, private router: Router, private security: SecurityService, private cookies: CookieService) { }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let token = localStorage.getItem('ArenaGestorToken')
        if (token) {
            req = req.clone({
                setHeaders: {
                    token: token
                }
            });
        } else {
            req = req.clone()
        }
        return next.handle(req)
    }

}