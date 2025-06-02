import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StopsRouteFormComponent } from './stops-route-form.component';

describe('StopsRouteFormComponent', () => {
  let component: StopsRouteFormComponent;
  let fixture: ComponentFixture<StopsRouteFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StopsRouteFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StopsRouteFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
