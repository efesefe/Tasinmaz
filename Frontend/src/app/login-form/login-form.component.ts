import { error } from "@angular/compiler/src/util";
import { Component, Input, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { Kullanici } from "../kullanici-table/kullanici";
import { LoginUser } from "../Models/login-user";
import { AuthService } from "../Services/auth.service";
import { KullaniciService } from "../Services/kullanici.service";
declare let alertify: any;
@Component({
  selector: "app-login-form",
  templateUrl: "./login-form.component.html",
  styleUrls: ["./login-form.component.css"],
})
export class LoginFormComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}
  title: string;
  loginUser: LoginUser;
  kullaniciAddForm: FormGroup;
  kullanici: Kullanici = new Kullanici();
  createKullaniciAddForm() {
    this.kullaniciAddForm = this.formBuilder.group({
      userName: ["", [Validators.required,Validators.email]],
      password: ["", [Validators.required,Validators.pattern('^(?=[^a-z]*[a-z])(?=.*[0-9])[A-Za-z\\d!$%@#£€*?&]{8,}$')]],
    });
  }
  ngOnInit() {
    this.createKullaniciAddForm();
  }

  login() {
    if (this.kullaniciAddForm.valid) {
      this.loginUser = Object.assign({}, this.kullaniciAddForm.value);
      this.authService.login(this.loginUser);
    }else
    {
      this.alerts = "zorunlu";
      this.pressed = true;
    }
  }
  alerts:string;
  pressed:boolean;
  passwordControl()
  {
    let pass:string;
    pass = this.kullaniciAddForm.get('password').value;
    if(pass.length < 8)
    {
      return 1;
    }
    if(!/\d/.test(pass))
    {
      return 2;
    }
    if(/^[A-Za-z]+$/.test(pass))
    {
      return 3;
    }
  }
}
