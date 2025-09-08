import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudMarks } from './stud-marks';

describe('StudMarks', () => {
  let component: StudMarks;
  let fixture: ComponentFixture<StudMarks>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudMarks]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudMarks);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
