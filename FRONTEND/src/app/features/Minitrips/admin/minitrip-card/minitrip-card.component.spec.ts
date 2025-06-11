import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MinitripCardComponent } from './minitrip-card.component';

describe('MinitripCardComponent', () => {
  let component: MinitripCardComponent;
  let fixture: ComponentFixture<MinitripCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MinitripCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MinitripCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
