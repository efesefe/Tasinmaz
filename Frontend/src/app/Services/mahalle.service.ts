import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Mahalle } from '../Models/mahalle';

@Injectable({
  providedIn: 'root'
})
export class MahalleService {

  constructor(private http:HttpClient) { }

  url = "http://localhost:5000/api/Mahalle/";

  getMahalle(id):Observable<Mahalle[]>
  {
    return this.http.get<Mahalle[]>(this.url + id);
  }
}
