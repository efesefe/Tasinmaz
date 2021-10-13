import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KullaniciEkleForm2Component } from './kullanici-ekle-form2.component';

describe('KullaniciEkleForm2Component', () => {
  let component: KullaniciEkleForm2Component;
  let fixture: ComponentFixture<KullaniciEkleForm2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KullaniciEkleForm2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KullaniciEkleForm2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
