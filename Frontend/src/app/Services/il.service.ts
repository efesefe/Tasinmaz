import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Il } from '../Models/il';

@Injectable({
  providedIn: 'root'
})
export class IlService {

  constructor(private http:HttpClient) { }

  url = "http://localhost:5000/api/Il/";
  getIller():Observable<Il[]>
  {
    return this.http.get<Il[]>(this.url);
  }
  getIl(id):Observable<Il>
  {
    return this.http.get<Il>(this.url + id);
  }
}
