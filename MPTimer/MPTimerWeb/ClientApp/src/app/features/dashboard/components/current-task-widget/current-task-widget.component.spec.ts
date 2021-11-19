import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrentTaskWidgetComponent } from './current-task-widget.component';

describe('CurrentTaskWidgetComponent', () => {
  let component: CurrentTaskWidgetComponent;
  let fixture: ComponentFixture<CurrentTaskWidgetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CurrentTaskWidgetComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CurrentTaskWidgetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
