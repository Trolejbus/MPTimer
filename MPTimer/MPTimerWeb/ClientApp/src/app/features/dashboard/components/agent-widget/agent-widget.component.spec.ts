import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentWidgetComponent } from './agent-widget.component';

describe('AgentWidgetComponent', () => {
  let component: AgentWidgetComponent;
  let fixture: ComponentFixture<AgentWidgetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentWidgetComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentWidgetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
