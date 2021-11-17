import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkTaskListComponent } from './components';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    WorkTaskListComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: 'list', component: WorkTaskListComponent },
      { path: '**', redirectTo: 'list' },
    ]),
  ],
})
export class WorkTasksModule { }
