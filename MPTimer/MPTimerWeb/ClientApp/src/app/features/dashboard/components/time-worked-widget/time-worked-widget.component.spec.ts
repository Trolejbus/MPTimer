import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeWorkedWidgetComponent } from './time-worked-widget.component';

describe('TimeWorkedComponent', () => {
  let component: TimeWorkedWidgetComponent;
  let fixture: ComponentFixture<TimeWorkedWidgetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimeWorkedWidgetComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TimeWorkedWidgetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
