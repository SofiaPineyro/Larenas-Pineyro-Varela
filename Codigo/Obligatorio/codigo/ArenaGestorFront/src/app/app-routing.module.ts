import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ArtistInsertComponent } from './routes/artist/artist-insert.component';
import { ArtistUpdateComponent } from './routes/artist/artist-update.component';
import { ArtistComponent } from './routes/artist/artist.component';
import { GenderInsertComponent } from './routes/genders/gender-insert.component';
import { GenderUpdateComponent } from './routes/genders/gender-update.component';
import { GendersComponent } from './routes/genders/genders.component';
import { HomeComponent } from './routes/home/home.component';
import { LoginComponent } from './routes/login/login.component';
import { AuthGuard } from './auth.guard'
import { SoloistComponent } from './routes/soloist/soloist.component';
import { SoloistInsertComponent } from './routes/soloist/soloist-insert.component';
import { SoloistUpdateComponent } from './routes/soloist/soloist-update.component';
import { BandComponent } from './routes/band/band.component';
import { BandInsertComponent } from './routes/band/band-insert.component';
import { BandUpdateComponent } from './routes/band/band-update.component';
import { UserComponent } from './routes/user/user.component';
import { UserInsertComponent } from './routes/user/user-insert.component';
import { UserUpdateComponent } from './routes/user/user-update.component';
import { ChangePasswordComponent } from './routes/change-password/change-password.component';
import { ImportexportComponent } from './routes/importexport/importexport.component';
import { ChangeDataComponent } from './routes/change-data/change-data.component';
import { MyTicketsComponent } from './routes/my-tickets/my-tickets.component';
import { MyConcertsComponent } from './routes/my-concerts/my-concerts.component';
import { ScanComponent } from './routes/scan/scan.component';
import { SellComponent } from './routes/sell/sell.component';
import { ConcertComponent } from './routes/concert/concert.component';
import { ConcertInsertComponent } from './routes/concert/concert-insert.component';
import { ConcertUpdateComponent } from './routes/concert/concert-update.component';
import { ConcertViewComponent } from './routes/concert/concert-view.component';
import { ConcertArtistViewComponent } from './routes/concert/concert-artist-view.component';
import { BuyComponent } from './routes/buy/buy.component';
import { ProtagonistBandsComponent } from './routes/protagonist/protagonist.bands.component';
import { ProtagonistSoloistsComponent } from './routes/protagonist/protagonist.soloists.component';
import { ProtagonistBandComponent } from './routes/protagonist/protagonist.band.component';
import { ProtagonistSoloistComponent } from './routes/protagonist/protagonist.soloist.component';

const routes: Routes = [

  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'cambiarcontrasena', component: ChangePasswordComponent, canActivate: [AuthGuard], data: { roles: ['Administrador', 'Espectador', 'Vendedor', 'Acomodador', 'Artista'] } },
  { path: 'misdatos', component: ChangeDataComponent, canActivate: [AuthGuard], data: { roles: ['Administrador', 'Espectador', 'Vendedor', 'Acomodador', 'Artista'] } },
  { path: 'mistickets', component: MyTicketsComponent, canActivate: [AuthGuard], data: { roles: ['Espectador'] } },
  { path: 'misconciertos', component: MyConcertsComponent, canActivate: [AuthGuard], data: { roles: ['Artista'] } },
  { path: 'tickets/scan', component: ScanComponent, canActivate: [AuthGuard], data: { roles: ['Acomodador'] } },
  { path: 'tickets/vender', component: SellComponent, canActivate: [AuthGuard], data: { roles: ['Vendedor'] } },
  { path: 'tickets/comprar/:id', component: BuyComponent, canActivate: [AuthGuard], data: { roles: ['Espectador'] } },
  { path: 'administracion/generos', component: GendersComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/generos/insertar', component: GenderInsertComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/generos/editar/:id', component: GenderUpdateComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/artistas', component: ArtistComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/artistas/insertar', component: ArtistInsertComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/artistas/editar/:id', component: ArtistUpdateComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/solistas', component: SoloistComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/solistas/insertar', component: SoloistInsertComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/solistas/editar/:id', component: SoloistUpdateComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/bandas', component: BandComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/bandas/insertar', component: BandInsertComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/bandas/editar/:id', component: BandUpdateComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/usuarios', component: UserComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/usuarios/insertar', component: UserInsertComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/usuarios/editar/:id', component: UserUpdateComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/importarexportar', component: ImportexportComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/conciertos', component: ConcertComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/conciertos/insertar', component: ConcertInsertComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/conciertos/editar/:id', component: ConcertUpdateComponent, canActivate: [AuthGuard], data: { roles: ['Administrador'] } },
  { path: 'administracion/conciertos/ver/:id', component: ConcertViewComponent },
  { path: 'administracion/conciertos/verbyartista/:id', component: ConcertArtistViewComponent },
  { path: 'protagonistas/bandas', component: ProtagonistBandsComponent },
  { path: 'protagonistas/solistas', component: ProtagonistSoloistsComponent },
  { path: 'protagonistas/bandas/ver/:id', component: ProtagonistBandComponent },
  { path: 'protagonistas/solistas/ver/:id', component: ProtagonistSoloistComponent },
  { path: '**', component: HomeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
