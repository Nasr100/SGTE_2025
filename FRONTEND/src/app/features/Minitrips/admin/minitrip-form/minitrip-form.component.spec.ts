import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MinitripFormComponent } from './minitrip-form.component';

describe('MinitripFormComponent', () => {
  let component: MinitripFormComponent;
  let fixture: ComponentFixture<MinitripFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MinitripFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MinitripFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
