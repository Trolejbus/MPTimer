import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { KnobModule } from 'primeng/knob';
import { ChartModule } from 'primeng/chart';
import { AgentWidgetComponent, DashboardComponent, TimeWorkedWidgetComponent, CurrentTaskWidgetComponent } from './components';

@NgModule({
  declarations: [
    DashboardComponent,
    TimeWorkedWidgetComponent,
    AgentWidgetComponent,
    CurrentTaskWidgetComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: '', component: DashboardComponent },
    ]),
    KnobModule,
    ChartModule,
  ]
})
export class DashboardModule { }
