import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KullaniciGuncelleForm1Component } from './kullanici-guncelle-form1.component';

describe('KullaniciGuncelleForm1Component', () => {
  let component: KullaniciGuncelleForm1Component;
  let fixture: ComponentFixture<KullaniciGuncelleForm1Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KullaniciGuncelleForm1Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KullaniciGuncelleForm1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
