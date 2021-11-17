import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkTaskListComponent } from './components';
import { RouterModule } from '@angular/router';
import { TableModule } from 'primeng/table';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  declarations: [
    WorkTaskListComponent,
  ],
  imports: [
    // Angular
    CommonModule,
    RouterModule.forChild([
      { path: 'list', component: WorkTaskListComponent },
      { path: '**', redirectTo: 'list' },
    ]),

    // Project
    SharedModule,

    // PrimeNg
    TableModule,
  ],
})
export class WorkTasksModule { }
