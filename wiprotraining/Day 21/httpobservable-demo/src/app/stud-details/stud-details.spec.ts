import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudDetails } from './stud-details';

describe('StudDetails', () => {
  let component: StudDetails;
  let fixture: ComponentFixture<StudDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudDetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
