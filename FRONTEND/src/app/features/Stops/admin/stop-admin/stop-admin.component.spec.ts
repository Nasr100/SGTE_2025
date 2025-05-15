import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StopAdminComponent } from './stop-admin.component';

describe('StopAdminComponent', () => {
  let component: StopAdminComponent;
  let fixture: ComponentFixture<StopAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StopAdminComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StopAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
