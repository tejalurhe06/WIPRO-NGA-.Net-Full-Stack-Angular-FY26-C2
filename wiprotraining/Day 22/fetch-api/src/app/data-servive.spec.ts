import { TestBed } from '@angular/core/testing';

import { DataServive } from './data-servive';

describe('DataServive', () => {
  let service: DataServive;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DataServive);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
