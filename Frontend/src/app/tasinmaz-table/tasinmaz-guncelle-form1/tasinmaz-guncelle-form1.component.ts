import { Component, OnInit } from "@angular/core";
import { Il } from "src/app/Models/il";
import { Ilce } from "src/app/Models/ilce";
import { IlceService } from "src/app/Services/ilce.service";
import { Mahalle } from "src/app/Models/mahalle";
import { MahalleService } from "src/app/Services/mahalle.service";
import { IlService } from "src/app/Services/il.service";
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormsModule,
} from "@angular/forms";
import { TasinmazService } from "src/app/Services/tasinmaz.service";
import { TasinmazPut } from "src/app/Models/tasinmaz-put";
import { Router } from "@angular/router";
import { ActivatedRoute } from "@angular/router";
import { Tasinmaz } from "../tasinmaz";
import { GetTasinmaz } from "src/app/Models/get-tasinmaz";
import { error } from "@angular/compiler/src/util";
@Component({
  selector: "app-tasinmaz-guncelle-form1",
  templateUrl: "./tasinmaz-guncelle-form1.component.html",
  styleUrls: ["./tasinmaz-guncelle-form1.component.css"],
})
export class TasinmazGuncelleForm1Component implements OnInit {
  constructor(
    private tasinmazService: TasinmazService,
    private ilService: IlService,
    private ilceService: IlceService,
    private mahalleService: MahalleService,
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute
  ) {}
  iller: Il[];
  ilceler: Ilce[];
  mahalleler: Mahalle[];
  ilId: number;
  ilceId: number;
  tasinmazGuncelleForm: FormGroup;
  putTas = new TasinmazPut();
  createTasinmazUpdateForm() {
    this.activatedRoute.params.subscribe((params) => {
      this.tasinmazService
        .getTasinmazById(params["tasinmazId"])
        .subscribe((tas) => {
          this.ilId = tas.ilID;
          this.ilceId = tas.ilceID;
          this.ilService.getIller().subscribe((ils) => {
            this.iller = ils;
            this.ilceService.getIller(tas.ilID).subscribe((ilces) => {
              this.ilceler = ilces;
              this.mahalleService.getMahalle(tas.ilceID).subscribe((mahs) => {
                this.mahalleler = mahs;
                this.tasinmazGuncelleForm = this.formBuilder.group({
                  tasinmazID: [tas.tasinmazID],
                  ilID: [tas.ilID, [Validators.required , Validators.min(1)]],
                  ilceID: [tas.ilceID, [Validators.required ]],
                  mahalleID: [tas.mahalleID, [Validators.required ]],
                  ada: [tas.ada, [Validators.required ]],
                  parsel: [tas.parsel, [Validators.required]],
                  nitelik: [tas.nitelik, Validators.required],
                  adres: [tas.adres, Validators.required],
                  isActive: true,
                });
              });
            });
          });
        });
    });
  }
  ngOnInit() {
    this.createTasinmazUpdateForm();
  }
  updateTasinmaz() {
    if (this.tasinmazGuncelleForm.valid) {
      this.putTas.tasinmazID =
        this.tasinmazGuncelleForm.get("tasinmazID").value;
      this.putTas.mahalleID = this.tasinmazGuncelleForm.get("mahalleID").value;
      this.putTas.ada = this.tasinmazGuncelleForm.get("ada").value;
      this.putTas.parsel = this.tasinmazGuncelleForm.get("parsel").value;
      this.putTas.nitelik = this.tasinmazGuncelleForm.get("nitelik").value;
      this.putTas.adres = this.tasinmazGuncelleForm.get("adres").value;
      this.putTas.isActive = this.tasinmazGuncelleForm.get("isActive").value;
      //this.putTas = Object.assign({} , this.tasinmazGuncelleForm.value);
      console.log(this.putTas);
      this.tasinmazService.updateTasinmaz(this.putTas);
    } else {
      alert("Tüm alanları doldurunuz...");
    }
  }
  ilsec(id) {
    this.ilceService.getIller(id).subscribe({
      next:x=>
      {
        this.ilceler = x;
      },
      error:err =>
      {
        this.ilceler = null;
      }
      
    });
    this.mahalleler = null;
    this.tasinmazGuncelleForm.get('ilceID').setValue(null);
    this.tasinmazGuncelleForm.get('mahalleID').setValue(null);
  }
  ilcesec(id) {
    this.mahalleService.getMahalle(id).subscribe( {
      next: z=>
      {
        this.mahalleler = z;
      },
      error:err=>{
        this.mahalleler = null;
      }
    });
    this.tasinmazGuncelleForm.get('mahalleID').setValue(null);
  }
}
