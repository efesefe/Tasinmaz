import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ilce } from '../Models/ilce';

@Injectable({
  providedIn: 'root'
})
export class IlceService {

  constructor(private http:HttpClient) { }

  url = "http://localhost:5000/api/Ilce/";
  getIller(id):Observable<Ilce[]>
  {
    return this.http.get<Ilce[]>(this.url + id);
  }
}
