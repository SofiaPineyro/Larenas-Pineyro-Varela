import { Component, OnInit } from '@angular/core';
import { SecurityService } from './services/security.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  constructor(private security: SecurityService){}

  ngOnInit(): void {
    this.security.GetLoggedUser().subscribe()
  }

  title = 'ArenaGestorFront';
}
