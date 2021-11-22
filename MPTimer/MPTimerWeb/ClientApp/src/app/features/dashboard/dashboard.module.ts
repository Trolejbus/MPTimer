import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { KnobModule } from 'primeng/knob';
import { ChartModule } from 'primeng/chart';
import { AgentWidgetComponent, DashboardComponent, TimeWorkedWidgetComponent, CurrentTaskWidgetComponent, WorkTimeLineComponent,
  WorkTimeLineViewComponent } from './components';
import { TooltipModule } from 'primeng/tooltip';

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
    KnobModule,
    ChartModule,
    TooltipModule,
  ]
})
export class DashboardModule { }
