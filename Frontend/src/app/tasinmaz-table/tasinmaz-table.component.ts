import { Component, NgModule, OnInit, ViewChild } from "@angular/core";
import { Tasinmaz } from "./tasinmaz";
import { TasinmazService } from "../Services/tasinmaz.service";
import { MatPaginatorModule, MatPaginator } from "@angular/material/paginator";
import { AuthService } from "../Services/auth.service";
import { ListParameters } from "../Models/list-parameters";
@Component({
  selector: "app-tasinmaz-table",
  templateUrl: "./tasinmaz-table.component.html",
  styleUrls: ["./tasinmaz-table.component.css"],
  providers: [TasinmazService],
})
export class TasinmazTableComponent implements OnInit {
  constructor(
    private tasinmazService: TasinmazService,
    private authService: AuthService
  ) {}
  title = "Taşınmaz listesi";
  tasinmazlar: Tasinmaz[];
  tas: Tasinmaz;
  lp: ListParameters = new ListParameters();
  kullanicisearch: string;
  messErr: string;
  ngOnInit() {
    this.getTasinmazlar(this.lp);
    
  }
  silTasinmaz(id: number) {
    if (confirm("Silmek istediğinize emin misiniz?")) {
      this.tasinmazService.deleteTasinmaz(id);
      var x = this.tasinmazlar.find((x) => x.tasinmazId === id);
      this.tasinmazlar.splice(this.tasinmazlar.indexOf(x), 1);
    }
  }
  getTasinmazlar(lisPar: ListParameters) {
    this.tasinmazService.getTasinmazlar(lisPar).subscribe((data) => {
      this.tasinmazlar = data;
      this.getTasinmazSayfaSayisi();
      
    });
  }
  nextPage() {
    if (this.lp.PageNumber + 1 <= this.saysay) {
      this.lp.PageNumber = this.lp.PageNumber + 1;
      this.searchChange();
    }
  }
  previousPage() {
    if (this.lp.PageNumber - 1 > 0) {
      this.lp.PageNumber = this.lp.PageNumber - 1;
      this.searchChange();
    }
  }
  searchChange() {
    this.lp.searchKey = this.kullanicisearch;
    this.getTasinmazlar(this.lp);
  }

  pageNumbers:any[];
  fillPageNumbers()
  {
    for (let index = 0; index < this.saysay; index++) {
      this.pageNumbers[index] = index + 1;
    }
  }
  selectedPage:number;
  changePageNumber(i)
  {
    this.lp.PageNumber = this.selectedPage;
    this.searchChange();
  }
  raporlaTasinmaz(tasinmaz: Tasinmaz) {
    this.tasinmazService.exportToExcell(tasinmaz);
  }

  get isAuthenticated() {
    return this.authService.loggedIn();
  }
  saysay: number;
  getTasinmazSayfaSayisi() {
    setTimeout(() => {
      this.tasinmazService.getTasinmazSayfaSayisi(this.lp).subscribe((saysay) => {
        this.saysay = saysay;
        this.fillPageNumbers();
      })
    }, 500);
  }
  printtest()
  {
    console.log(this.saysay)
  }
}
