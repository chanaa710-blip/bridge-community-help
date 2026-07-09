import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRequest } from './add-request';

describe('AddRequest', () => {
  let component: AddRequest;
  let fixture: ComponentFixture<AddRequest>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddRequest],
    }).compileComponents();

    fixture = TestBed.createComponent(AddRequest);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
