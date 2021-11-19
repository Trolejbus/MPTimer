import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RouterModule } from '@angular/router';
import { KnobModule } from 'primeng/knob';
import { ChartModule } from 'primeng/chart';
import { TimeWorkedWidgetComponent } from './dashboard/components/time-worked-widget/time-worked-widget.component';

@NgModule({
  declarations: [
    DashboardComponent,
    TimeWorkedWidgetComponent
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
