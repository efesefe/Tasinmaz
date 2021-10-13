import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Kullanici } from '../kullanici-table/kullanici';
import { Workbook } from 'exceljs';
import * as fs from 'file-saver';
import { ListParameters } from '../Models/list-parameters';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class KullaniciService {

  constructor(private http:HttpClient,private router:Router) { }
  url = "http://localhost:5000/api/Kullanici/";
  getKullanicilar(listParameters : ListParameters) : Observable<Kullanici[]>
  {
    const data = JSON.stringify(listParameters);
    const headers = { "content-type": "application/json"}
    return this.http.post<Kullanici[]>(this.url + "pag", data , {'headers' : headers});
  }
  getKullanici(id):Observable<Kullanici>
  {
    return this.http.get<Kullanici>(this.url+ id);
  }
  addKullanici(kullanici:Kullanici)
  {
    const body = JSON.stringify(kullanici);
    const headers = { 'content-type': 'application/json'}  
    this.http.post<Kullanici>(this.url + 'reg/reg' , body , {'headers' : headers}).subscribe(data => {
      console.log("Basarili" + data);
      this.router.navigateByUrl('/kullanicilar')
    });
  }
  deleteKullanici(deleteId: number){
    this.http.delete(this.url + deleteId).subscribe(x => {
      alert("silindi")
    });
  }
  updateKullanici(kullanici : Kullanici)
  {
    const body = JSON.stringify(kullanici);
    console.log(body);
    const headers = { 'content-type': 'application/json'}  
    this.http.put<Kullanici>(this.url + 'Put' , kullanici , {'headers' : headers}).subscribe( {
      next:x =>{
        alert("Guncelleme basarili " + x);
        this.router.navigateByUrl("/kullanicilar")
      },
      error:err=>
      {
        alert("Guncelleme başarısız");
      }
    })
  }
  exportToExcell(kullaniciData:Kullanici , role : string , id:string)
  {
    const title =kullaniciData.isim + " isimli kullanıcının raporu";
    const header = ["id" , "isim" , "soyisim" , "email" , "role"];
    const data = [id,
                  kullaniciData.isim,
                  kullaniciData.soyisim,
                  kullaniciData.email,
                  role
    ];
    let workbook = new Workbook();
    let worksheet = workbook.addWorksheet('Kullanıcı rapor');

    worksheet.mergeCells('C1', 'F4');
    let titleRow = worksheet.getCell('C1');
    titleRow.value = title
    titleRow.font = {
      name: 'Calibri',
      size: 16,
      underline: 'single',
      bold: true,
      color: { argb: '0085A3' }
    }
    titleRow.alignment = { vertical: 'middle', horizontal: 'center' }
    let headerRow = worksheet.addRow(header);
    headerRow.eachCell((cell, number) => {
      cell.fill = {
        type: 'pattern',
        pattern: 'solid',
        fgColor: { argb: '4167B8' },
        bgColor: { argb: '' }
      }
      cell.font = {
        bold: true,
        color: { argb: 'FFFFFF' },
        size: 12
      }
    })
    worksheet.addRow(data).fill;
    workbook.xlsx.writeBuffer().then((data) => {
      let blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      fs.saveAs(blob, title + '.xlsx');
    })
  }
  xnumber:any;
  getKullaniciSayisi():Observable<any>
  {
    return this.http.get(this.url + "getKullaniciSayisi");
  }
}