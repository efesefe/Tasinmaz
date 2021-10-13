import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KullaniciTableComponent } from './kullanici-table.component';

describe('KullaniciTableComponent', () => {
  let component: KullaniciTableComponent;
  let fixture: ComponentFixture<KullaniciTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KullaniciTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KullaniciTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
