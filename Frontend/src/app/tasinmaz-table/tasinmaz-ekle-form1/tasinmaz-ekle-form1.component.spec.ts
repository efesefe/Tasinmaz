import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TasinmazEkleForm1Component } from './tasinmaz-ekle-form1.component';

describe('TasinmazEkleForm1Component', () => {
  let component: TasinmazEkleForm1Component;
  let fixture: ComponentFixture<TasinmazEkleForm1Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TasinmazEkleForm1Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TasinmazEkleForm1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
