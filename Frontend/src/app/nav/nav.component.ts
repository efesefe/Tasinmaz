import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../Services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(private authService : AuthService , private router:Router) { }
  
  ngOnInit() {
  }

  get isEntered()
  {
    console.log(this.authService.loggedIn());
    return this.authService.loggedIn();
  }
  get isAdmin()
  {
    return this.authService.isAdmin();
  }
  logout()
  {
    if(confirm("Çıkmak istediğinize emin misiniz?"))
    {
      this.authService.logOut();
    }
  }
}
