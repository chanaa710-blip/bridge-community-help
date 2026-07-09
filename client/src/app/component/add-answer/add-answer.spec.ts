import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAnswer } from './add-answer';

describe('AddAnswer', () => {
  let component: AddAnswer;
  let fixture: ComponentFixture<AddAnswer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddAnswer],
    }).compileComponents();

    fixture = TestBed.createComponent(AddAnswer);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
