import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { SecurityService } from './services/security.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private security: SecurityService) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    let roles = route.data['roles'] as Array<string>;
    if (roles == undefined) {
      return true;
    }
    return this.security.GetLoggedUser().pipe(map(x => {
      if (this.security.isAuthorizedByRoles(roles)) {
        return true;
      }
      return this.router.createUrlTree(["/home"]);
    }))
  }

}
