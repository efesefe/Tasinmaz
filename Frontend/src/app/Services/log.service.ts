import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Log } from '../Models/log';

import { Workbook } from 'exceljs';
import * as fs from 'file-saver';
import { ListParameters } from '../Models/list-parameters';


@Injectable({
  providedIn: 'root'
})
export class LogService {

  constructor(private http:HttpClient) { }
  url = "http://localhost:5000/api/Log/";
  
  getLoglar(listParameters:ListParameters) : Observable<Log[]>
  {
    const data = JSON.stringify(listParameters);
    const headers = { "content-type": "application/json"}
    return this.http.post<Log[]>(this.url + "getmemylogs/ss" ,data  , {'headers':headers});
  }
  exportToExcell(logData:Log)
  {
    const title = logData.logID + " id'li logun raporu";
    const header = ["id" , "ip" , "Log islemi" , "Log aciklama" , "Log durumu" , "Log zamani"];
    const data = [logData.logID , logData.logIP , logData.logIslemi,logData.logAciklama,
      logData.logDurumu , logData.logZaman
    ];
    let workbook = new Workbook();
    let worksheet = workbook.addWorksheet('Log rapor');

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
