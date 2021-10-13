import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { KullaniciEkleForm2Component } from "./kullanici-table/kullanici-ekle-form2/kullanici-ekle-form2.component";
import { KullaniciGuncelleForm1Component } from "./kullanici-table/kullanici-guncelle-form1/kullanici-guncelle-form1.component";
import { KullaniciTableComponent } from "./kullanici-table/kullanici-table.component";
import { LogTableComponent } from "./log-table/log-table.component";
import { LoginFormComponent } from "./login-form/login-form.component";
import { TasinmazEkleForm1Component } from "./tasinmaz-table/tasinmaz-ekle-form1/tasinmaz-ekle-form1.component";
import { TasinmazGuncelleForm1Component } from "./tasinmaz-table/tasinmaz-guncelle-form1/tasinmaz-guncelle-form1.component";
import { TasinmazTableComponent } from "./tasinmaz-table/tasinmaz-table.component";

const routes: Routes = [
  { path: "", redirectTo: "login", pathMatch: "full" },
  { path: "tasinmaz", component: TasinmazTableComponent },
  { path: "tasinmaz/tasinmazEkle", component: TasinmazEkleForm1Component },
  {
    path: "kullanicilar/kullaniciekle",
    component: KullaniciEkleForm2Component,
  },
  { path: "kullanicilar", component: KullaniciTableComponent },
  {
    path: "kullanicilar/guncelle/:kullaniciID",
    component: KullaniciGuncelleForm1Component,
  },
  {
    path: "tasinmaz/guncelle/:tasinmazId",
    component: TasinmazGuncelleForm1Component,
  },
  {
    path: "log",
    component: LogTableComponent,
  },
  {
    path: "login",
    component: LoginFormComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
