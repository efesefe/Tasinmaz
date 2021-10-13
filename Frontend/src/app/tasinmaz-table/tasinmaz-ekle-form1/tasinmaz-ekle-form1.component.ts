import { Component, OnInit } from "@angular/core";
import { Il } from "src/app/Models/il";
import { Ilce } from "src/app/Models/ilce";
import { IlceService } from "src/app/Services/ilce.service";
import { Mahalle } from "src/app/Models/mahalle";
import { MahalleService } from "src/app/Services/mahalle.service";
import { IlService } from "src/app/Services/il.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { TasinmazService } from "src/app/Services/tasinmaz.service";
import { TasinmazPost } from "src/app/Models/tasinmaz-post";
import { Router } from "@angular/router";

@Component({
  selector: "app-tasinmaz-ekle-form1",
  templateUrl: "./tasinmaz-ekle-form1.component.html",
  styleUrls: ["./tasinmaz-ekle-form1.component.css"],
})
export class TasinmazEkleForm1Component implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private ilService: IlService,
    private ilceService: IlceService,
    private mahalleService: MahalleService,
    private tasinmazService: TasinmazService,
    private router: Router
  ) {}
  alert: string;
  pressed: boolean;
  iller: Il[];
  ilceler: Ilce[];
  mahalleler: Mahalle[];
  ilid:number = null;
  ilceid:number = null;

  tasinmazAddForm: FormGroup;
  tasinmazPost: TasinmazPost = new TasinmazPost();
  createTasinmazAddForm() {
    this.tasinmazAddForm = this.formBuilder.group({
      mahalleID: ["", Validators.required],
      ada: ["", [Validators.required, Validators.min(1)]],
      parsel: ["", [Validators.required, Validators.min(1)]],
      nitelik: ["", Validators.required],
      adres: ["", Validators.required],
      isActive: true,
    });
  }

  addTasinmaz() {
    if (this.tasinmazAddForm.valid) {
      this.tasinmazPost = Object.assign({}, this.tasinmazAddForm.value);
      this.tasinmazService.addTasinmaz(this.tasinmazPost);
      this.router.navigateByUrl("/tasinmaz");
    } 
    else {
      this.alert = "zorunlu";
      this.pressed = true;
    }
  }
  rev()
  {
    this.alert = null;
    this.pressed = false;
  }

  ngOnInit() {
    this.ilService.getIller().subscribe((data) => {
      this.iller = data;
    });
    this.createTasinmazAddForm();
  }
  onOptionsSelected(id) {
    this.ilceService.getIller(id).subscribe((data) => {
      this.ilceler = data;
    });
    this.mahalleler = null;
  }
  ilceSecildi(id) {
    this.mahalleService.getMahalle(id).subscribe((data) => {
      this.mahalleler = data;
    });
  }
}
