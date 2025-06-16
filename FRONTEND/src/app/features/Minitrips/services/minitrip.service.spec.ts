import { TestBed } from '@angular/core/testing';

import { MinitripService } from './minitrip.service';

describe('MinitripService', () => {
  let service: MinitripService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MinitripService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
