import { Component, OnInit, NgModule} from "@angular/core";
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { KullaniciService } from "src/app/Services/kullanici.service";
import { Kullanici } from "../kullanici";
import { Router } from "@angular/router";

@Component({
  selector: "app-kullanici-guncelle-form1",
  templateUrl: "./kullanici-guncelle-form1.component.html",
  styleUrls: ["./kullanici-guncelle-form1.component.css"],
})
@NgModule({
  imports: [FormsModule, ReactiveFormsModule],
})
export class KullaniciGuncelleForm1Component implements OnInit {
  [x: string]: any;

  constructor(
    private formBuilder: FormBuilder,
    private kullaniciService: KullaniciService,
    private activatedRoute: ActivatedRoute,
    private route: Router
  ) {}

  kullaniciAddForm: FormGroup;
  kullanici: Kullanici = new Kullanici();
  eskiBilgiliKullanici: Kullanici = new Kullanici();
  thisId :number;
  updateKullanici() {
    this.activatedRoute.params.subscribe((params) => {
      this.kullaniciService.getKullanici(params["kullaniciID"]).subscribe(x => 
        {
          this.kullaniciAddForm = this.formBuilder.group({
            kullaniciID: params["kullaniciID"],
            isim: x.isim,
            soyisim: x.soyisim,
            email: [x.email,[Validators.email]],
            password: [x.password,[Validators.pattern('^(?=[^a-z]*[a-z])(?=.*[0-9])[A-Za-z\\d!$%@#£€*?&]{8,}$')]],
            role: x.role,
            isActive: true,
          });
        });
    });
  }

  ngOnInit() {
    this.updateKullanici();
  }
  guncelle() {
    if (this.kullaniciAddForm.valid) {
      this.kullanici.KullaniciID=parseInt(this.kullaniciAddForm.get('kullaniciID').value);
      this.kullanici.isim = this.kullaniciAddForm.get('isim').value;
      this.kullanici.soyisim = this.kullaniciAddForm.get('soyisim').value;
      this.kullanici.email = this.kullaniciAddForm.get('email').value;
      this.kullanici.password = this.kullaniciAddForm.get('password').value;
      this.kullanici.role = this.kullaniciAddForm.get('role').value;
      this.kullanici.isActive = this.kullaniciAddForm.get('isActive').value;
      console.log(this.kullanici);
      this.kullaniciService.updateKullanici(this.kullanici);
    }else{
      alert("Tüm alanları doldurunuz")
    }
  }
}
