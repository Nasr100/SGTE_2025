import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministartionDetailComponent } from './administartion-detail.component';

describe('AdministartionDetailComponent', () => {
  let component: AdministartionDetailComponent;
  let fixture: ComponentFixture<AdministartionDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdministartionDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdministartionDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
