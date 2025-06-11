import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeTripsComponent } from './home-trips.component';

describe('HomeTripsComponent', () => {
  let component: HomeTripsComponent;
  let fixture: ComponentFixture<HomeTripsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomeTripsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomeTripsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
