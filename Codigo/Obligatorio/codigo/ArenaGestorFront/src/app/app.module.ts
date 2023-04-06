import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './layout/header/header.component';
import { HomeComponent } from './routes/home/home.component';
import { SecurityService } from './services/security.service';
import { LoginComponent } from './routes/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Events } from './app.events';
import { CookieService } from 'ngx-cookie-service';
import { AppHttpInterceptor } from './app.interceptor';
import { UserInfoComponent } from './layout/user-info/user-info.component';
import { GendersComponent } from './routes/genders/genders.component';
import { GenderInsertComponent } from './routes/genders/gender-insert.component';
import { GenderUpdateComponent } from './routes/genders/gender-update.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ToastrModule } from 'ngx-toastr';
import { ArtistComponent } from './routes/artist/artist.component';
import { ArtistInsertComponent } from './routes/artist/artist-insert.component';
import { ArtistUpdateComponent } from './routes/artist/artist-update.component';
import { SoloistComponent } from './routes/soloist/soloist.component';
import { SoloistInsertComponent } from './routes/soloist/soloist-insert.component';
import { SoloistUpdateComponent } from './routes/soloist/soloist-update.component';
import { BandComponent } from './routes/band/band.component';
import { BandInsertComponent } from './routes/band/band-insert.component';
import { BandUpdateComponent } from './routes/band/band-update.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { CommonModule } from '@angular/common';
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

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    LoginComponent,
    UserInfoComponent,
    GendersComponent,
    GenderInsertComponent,
    GenderUpdateComponent,
    ArtistComponent,
    ArtistInsertComponent,
    ArtistUpdateComponent,
    SoloistComponent,
    SoloistInsertComponent,
    SoloistUpdateComponent,
    BandComponent,
    BandInsertComponent,
    BandUpdateComponent,
    UserComponent,
    UserInsertComponent,
    UserUpdateComponent,
    ChangePasswordComponent,
    ImportexportComponent,
    ChangeDataComponent,
    MyTicketsComponent,
    MyConcertsComponent,
    ScanComponent,
    SellComponent,
    ConcertComponent,
    ConcertInsertComponent,
    ConcertUpdateComponent,
    ConcertViewComponent,
    ConcertArtistViewComponent,
    BuyComponent,
    ProtagonistBandsComponent,
    ProtagonistSoloistsComponent,
    ProtagonistBandComponent,
    ProtagonistSoloistComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    BsDatepickerModule.forRoot(),
    ReactiveFormsModule,
    NgMultiSelectDropDownModule.forRoot(),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AppHttpInterceptor, multi: true },
    SecurityService,
    Events,
    CookieService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
