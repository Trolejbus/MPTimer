import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkTaskFormComponent } from './work-task-form.component';

describe('WorkTaskFormComponent', () => {
  let component: WorkTaskFormComponent;
  let fixture: ComponentFixture<WorkTaskFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkTaskFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkTaskFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
