import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TasinmazGuncelleForm1Component } from './tasinmaz-guncelle-form1.component';

describe('TasinmazGuncelleForm1Component', () => {
  let component: TasinmazGuncelleForm1Component;
  let fixture: ComponentFixture<TasinmazGuncelleForm1Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TasinmazGuncelleForm1Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TasinmazGuncelleForm1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
