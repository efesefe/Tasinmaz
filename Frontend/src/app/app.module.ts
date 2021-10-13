import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { TasinmazTableComponent } from './tasinmaz-table/tasinmaz-table.component';

import { HttpClientModule } from '@angular/common/http';
import { KullaniciTableComponent } from './kullanici-table/kullanici-table.component';
import { KullaniciEkleForm2Component } from './kullanici-table/kullanici-ekle-form2/kullanici-ekle-form2.component';

import { FormsModule , ReactiveFormsModule } from '@angular/forms';
import { KullaniciGuncelleForm1Component } from './kullanici-table/kullanici-guncelle-form1/kullanici-guncelle-form1.component';
import { TasinmazEkleForm1Component } from './tasinmaz-table/tasinmaz-ekle-form1/tasinmaz-ekle-form1.component';
import { TasinmazGuncelleForm1Component } from './tasinmaz-table/tasinmaz-guncelle-form1/tasinmaz-guncelle-form1.component';
import { LogTableComponent } from './log-table/log-table.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { Ng2SearchPipeModule } from "ng2-search-filter";
import { NgxPaginationModule } from 'ngx-pagination';
@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    TasinmazTableComponent,
    KullaniciEkleForm2Component,
    KullaniciTableComponent,
    KullaniciGuncelleForm1Component,
    TasinmazEkleForm1Component,
    TasinmazGuncelleForm1Component,
    LogTableComponent,
    LoginFormComponent
  ],
  imports: [
    BrowserModule,
    NgxPaginationModule,
    AppRoutingModule,
    HttpClientModule,
    Ng2SearchPipeModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
