import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { KullaniciService } from './kullanici.service';

@Injectable({
  providedIn: 'root'
})
export class PageService {

  constructor(private kullaniciService:KullaniciService) { }
  pageCount:number;
  sayfaSayisiHesapla():Observable<number>
  {
    return this.kullaniciService.getKullaniciSayisi();
  }
}
