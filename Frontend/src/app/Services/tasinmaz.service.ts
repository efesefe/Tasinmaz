import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { GetTasinmaz } from "../Models/get-tasinmaz";
import { TasinmazPost } from "../Models/tasinmaz-post";
import { TasinmazPut } from "../Models/tasinmaz-put";
import { Tasinmaz } from "../tasinmaz-table/tasinmaz";
import { Workbook } from 'exceljs';
import * as fs from 'file-saver';
import { ListParameters } from "../Models/list-parameters";
import { Router } from "@angular/router";
@Injectable({
  providedIn: "root",
})
export class TasinmazService {
  constructor(private http: HttpClient,private router:Router) {}

  url = "http://localhost:5000/api/Tasinmaz/";

  getTasinmazlar(listParameters:ListParameters): Observable<Tasinmaz[]> {
    const data = JSON.stringify(listParameters);
    const headers = { "content-type": "application/json"}
    return this.http.post<Tasinmaz[]>(this.url + "pag/pag" , data , {'headers' : headers});
  }
  getTasinmazById(id: number): Observable<GetTasinmaz> {
    return this.http.get<GetTasinmaz>(this.url + id);
  }
  deleteTasinmaz(deleteId: number) {
    this.http.delete(this.url + deleteId).subscribe((data) => {
      console.log("Deletion successful");
    });
  }
  addTasinmaz(tasinmaz: TasinmazPost) {
    const body = JSON.stringify(tasinmaz);
    const headers = { "content-type": "application/json" };
    this.http
      .post<TasinmazPost>(this.url + "Post", body, { headers: headers })
      .subscribe((data) => {
        console.log("Basarili " + data);
      });
  }
  updateTasinmaz(tasinmaz: TasinmazPut) 
  {
     const body = JSON.stringify(tasinmaz);
     console.log(body);
     const headers = { "content-type": "application/json" };
    this.http.put<Tasinmaz>(this.url + 'Put' , body , {headers:headers})
      .subscribe({
        next: x =>{
          alert("Taşınmaz güncellendi");
          this.router.navigateByUrl("/tasinmaz");
        },
        error:err=>{
          console.log(tasinmaz);
          alert("Büyük başarısızlıklar söz konusu");
        }
      });
  }

  getTasinmazSayfaSayisi(listParameters:ListParameters) : Observable<any>
    {
      const body = JSON.stringify(listParameters);
      const headers = { "content-type": "application/json" };
      return this.http.post<any>(this.url + "pagnum/pagnum", body , {headers:headers});
    }

  exportToExcell(tasinmazData:Tasinmaz)
  {
    const title =tasinmazData.tasinmazId + " id'li tasinmazin raporu";
    const header = ["id" , "il" , "ilçe" , "mahalle" , "ada" , "parsel" , "nitelik" , "adres"];
    const data = [tasinmazData.tasinmazId,
                  tasinmazData.tasinmazIl,
                  tasinmazData.tasinmazIlce,
                  tasinmazData.tasinmazMahalle,
                  tasinmazData.ada,
                  tasinmazData.parsel,
                  tasinmazData.nitelik,
                  tasinmazData.adres
    ];
    let workbook = new Workbook();
    let worksheet = workbook.addWorksheet('Tasinmaz rapor');

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
}
