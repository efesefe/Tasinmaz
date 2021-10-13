import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { LoginUser } from "../Models/login-user";
import { JwtHelper, tokenNotExpired } from "angular2-jwt";
import { Router } from "@angular/router";
import { httpFactory } from "@angular/http/src/http_module";
import { Observable } from "rxjs";
import { Kullanici } from "../kullanici-table/kullanici";
import { error } from "@angular/compiler/src/util";
declare let alertify: any;
@Injectable({
  providedIn: "root",
})
export class AuthService {
  constructor(private httpClient: HttpClient, private router: Router) {}
  userToken: any;
  decodedToken: any;
  jwtHelper: JwtHelper = new JwtHelper();
  userRole: string;
  mydata:boolean;
  url = "http://localhost:5000/api/Login/Login";
  login(loginUser: LoginUser) {
    const headers = { "content-type": "application/json" };
    let data = JSON.stringify(loginUser);
    this.httpClient
      .post(this.url, data, {
        responseType: "text",
        headers: headers,
      })
      .subscribe({
        next:x =>{
          this.decodedToken = this.jwtHelper.decodeToken(x);
          this.userRole = this.decodedToken.role;
          this.saveToken(x , this.userRole);
          this.userToken = x;
          alert("Giriş başarılı")
          this.router.navigateByUrl("/tasinmaz");
        },
        error:err =>{
          alert("Giriş başarısız")
          console.log("olmaz");
        }
      }
        
      );
  }
  get getRole()
  {
    return this.userRole;
  }
  saveToken(token: string , role) {
    localStorage.setItem("token", token);
    localStorage.setItem("role", role);
  }
  logOut() {
    localStorage.removeItem("token");
    localStorage.removeItem("role");
    this.userToken = null;
    this.userRole = null;
    this.router.navigateByUrl("login");
  }
  loggedIn() {
    this.mydata = tokenNotExpired('token');
    return this.mydata;
  }
  isAdmin()
  {
    return localStorage.getItem('role') == "Admin";
  }
  get token() {
    return localStorage.getItem("token");
  }
  getCurrentUserId() {
    return this.jwtHelper.decodeToken(localStorage.getItem("token")).nameid;
  }
}
