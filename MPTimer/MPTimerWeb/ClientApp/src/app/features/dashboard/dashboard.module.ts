import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { KnobModule } from 'primeng/knob';
import { ChartModule } from 'primeng/chart';
import { AgentWidgetComponent, DashboardComponent, TimeWorkedWidgetComponent, CurrentTaskWidgetComponent, WorkTimeLineComponent,
  WorkTimeLineViewComponent } from './components';
import { TooltipModule } from 'primeng/tooltip';
import { CalendarModule } from 'primeng/calendar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    DashboardComponent,
    TimeWorkedWidgetComponent,
    AgentWidgetComponent,
    CurrentTaskWidgetComponent,
    WorkTimeLineComponent,
    WorkTimeLineViewComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: '', component: DashboardComponent },
    ]),
    FormsModule,
    ReactiveFormsModule,
    KnobModule,
    ChartModule,
    TooltipModule,
    CalendarModule,
  ],
})
export class DashboardModule { }
