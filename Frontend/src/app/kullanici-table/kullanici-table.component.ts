import { Component, NgModule, OnInit } from "@angular/core";
import { ListParameters } from "../Models/list-parameters";
import { AuthService } from "../Services/auth.service";
import { KullaniciService } from "../Services/kullanici.service";
import { PageService } from "../Services/page.service";
import { Kullanici } from "./kullanici";

@Component({
  selector: "app-kullanici-table",
  templateUrl: "./kullanici-table.component.html",
  styleUrls: ["./kullanici-table.component.css"],
  providers: [KullaniciService],
})
export class KullaniciTableComponent implements OnInit {
  constructor(
    private kullaniciService: KullaniciService,
    private authService: AuthService
  ) {}
  p: number;
  kullanicilar: Kullanici[];
  title = "Kullanıcı tablosu";
  roleStr: string;
  listParameters: ListParameters = new ListParameters();
  ngOnInit() {
    this.kullaniciService
      .getKullanicilar(this.listParameters)
      .subscribe((data) => {
        this.kullanicilar = data.sort();
      });
  }
  silKullanici(id: number) {
    if (confirm("Silmek istediğinize emin misiniz?")) {
      var x = this.kullanicilar.find((t) => t.KullaniciID === id);
      this.kullanicilar.splice(this.kullanicilar.indexOf(x), 1);
      this.kullaniciService.deleteKullanici(id);
    }
  }
  kullaniciRapor(kullanici: Kullanici, role, idStr) {
    if (role > 0) {
      this.roleStr = "Normal kullanıcı";
    } else {
      this.roleStr = "Admin";
    }

    this.kullaniciService.exportToExcell(kullanici, this.roleStr, idStr);
  }
  get isAuthenticated() {
    return this.authService.loggedIn();
  }
  saysay: number;
  kullanicisearch: string;
  nextPage() {
    this.listParameters.PageNumber = this.listParameters.PageNumber + 1;
    this.changedSearch();
  }
  previousPage() {
    if (this.listParameters.PageNumber - 1 > 0) {
      this.listParameters.PageNumber = this.listParameters.PageNumber - 1;
      this.changedSearch();
    }
  }
  // page() {
  //   this.pageService.sayfaSayisiHesapla().subscribe((data) => {
  //     this.saysay = data;
  //   });
  //   return (this.saysay = this.saysay / 5);
  // }
  changedSearch() {
    this.listParameters.searchKey = this.kullanicisearch;
    this.kullaniciService
      .getKullanicilar(this.listParameters)
      .subscribe((data) => {
        this.kullanicilar = data.sort();
      });
  }
}
