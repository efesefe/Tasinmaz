import { Component, NgModule, OnInit } from "@angular/core";
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from "@angular/forms";
import { KullaniciService } from "src/app/Services/kullanici.service";
import { Kullanici } from "../kullanici";
import { Router } from "@angular/router";
@Component({
  selector: "app-kullanici-ekle-form2",
  templateUrl: "./kullanici-ekle-form2.component.html",
  styleUrls: ["./kullanici-ekle-form2.component.css"],
})
@NgModule({
  imports: [FormsModule, ReactiveFormsModule],
})
export class KullaniciEkleForm2Component implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private kullaniciService: KullaniciService,
    private router: Router
  ) {}
    alerts:string;
    pressed:boolean;
  kullaniciAddForm: FormGroup;
  kullanici: Kullanici = new Kullanici();
  createKullaniciAddForm() {
    this.kullaniciAddForm = this.formBuilder.group({
      isim: ["", Validators.required],
      soyisim: ["", Validators.required],
      email: ["", [Validators.required,Validators.email]],
      password: ["", [Validators.required,Validators.pattern('^(?=[^a-z]*[a-z])(?=.*[0-9])[A-Za-z\\d!$%@#£€*?&]{8,}$')]],
      role: [true, Validators.required],
      isActive: true,
    });
  }

  ngOnInit() {
    this.createKullaniciAddForm();
  }
  add() {
    if (this.kullaniciAddForm.valid) {
      this.kullanici = Object.assign({}, this.kullaniciAddForm.value);
      this.kullaniciService.addKullanici(this.kullanici);
    }else
    {
      this.alerts = "zorunlu";
      this.pressed = true;
    }
  }
}
