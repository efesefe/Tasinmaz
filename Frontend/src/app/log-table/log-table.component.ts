import { Component, OnInit } from '@angular/core';
import { ListParameters } from '../Models/list-parameters';
import { Log } from '../Models/log';
import { LogService } from '../Services/log.service';

@Component({
  selector: 'app-log-table',
  templateUrl: './log-table.component.html',
  styleUrls: ['./log-table.component.css'],
  providers:[LogService]
})

export class LogTableComponent implements OnInit {

  constructor(private logService : LogService) { }
  lp:ListParameters = new ListParameters();
  kullanicisearch:string;
  title = 'Loglar';
  loglar : Log[];
  ngOnInit()
  {
    this.logService.getLoglar(this.lp).subscribe(data => {
      this.loglar = data;
    });
  }
  raporla(log:Log)
  {
    this.logService.exportToExcell(log);
  }
  changedSearch() {
    this.lp.searchKey = this.kullanicisearch;
    this.logService
      .getLoglar(this.lp)
      .subscribe((data) => {
        this.loglar = data;
      });
  }
  nextPage() {
    this.lp.PageNumber = this.lp.PageNumber + 1;
    this.changedSearch();
  }
  previousPage() {
    if (this.lp.PageNumber - 1 > 0) {
      this.lp.PageNumber = this.lp.PageNumber - 1;
      this.changedSearch();
    }
  }
}
