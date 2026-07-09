import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestDashboard } from './request-dashboard';

describe('RequestDashboard', () => {
  let component: RequestDashboard;
  let fixture: ComponentFixture<RequestDashboard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RequestDashboard],
    }).compileComponents();

    fixture = TestBed.createComponent(RequestDashboard);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
