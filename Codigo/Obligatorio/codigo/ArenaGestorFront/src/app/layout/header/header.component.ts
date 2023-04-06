import { Component, OnInit } from '@angular/core';
import { Events } from 'src/app/app.events';
import { SecurityLoggedUser } from 'src/app/models/Security/SecurityLoggedUser';
import { SecurityService } from 'src/app/services/security.service';
import { menu } from '../menu/menu';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  menuOptions: any[];

  isLogged: Boolean
  user?: SecurityLoggedUser

  constructor(private events: Events, private security: SecurityService) {
    this.isLogged = false
    this.menuOptions = []
  }

  ngOnInit(): void {
    this.events.LoggedStateChanged.subscribe(state => {
      this.isLogged = state;
      if (this.isLogged) {
        this.getUser()
      } else {
        this.getMenuOptions()
      }
    })
    this.getMenuOptions()
  }

  getUser() {
    this.security.GetLoggedUser().subscribe(user => {
      this.isLogged = true;
      this.user = user
      this.getMenuOptions()
    })
  }

  getMenuOptions() {
    let optionsToFilter: any = menu;
    let newMenu: any[];
    newMenu = []
    for (let i = 0; i < optionsToFilter.length; i++) {
      const element = optionsToFilter[i];
      if (element.submenu) {
        let newSubmenu: any[]
        newSubmenu = this.filterSubmenu(element.submenu);
        if (newSubmenu.length > 0) {
          element.submenu = newSubmenu;
          newMenu.push(element)
        }
      } else {
        if (this.mustAddElement(element)) {
          newMenu.push(element)
        }
      }
    }

    this.menuOptions = newMenu
  }

  filterSubmenu(submenu: any[]) {
    let newSubmenu: any[]
    newSubmenu = []
    for (let i = 0; i < submenu.length; i++) {
      const element = submenu[i];
      if (this.mustAddElement(element)) {
        newSubmenu.push(element)
      }
    }
    return newSubmenu;
  }

  mustAddElement(menuOption: any): boolean {
    if (!menuOption.roles) {
      return true;
    }
    return this.security.isAuthorizedByRoles(menuOption.roles);
  }

}
