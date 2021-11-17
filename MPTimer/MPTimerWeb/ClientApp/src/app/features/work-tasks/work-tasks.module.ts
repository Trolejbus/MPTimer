import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkTaskFormComponent, WorkTaskListComponent } from './components';
import { RouterModule } from '@angular/router';
import { TableModule } from 'primeng/table';
import { SharedModule } from '../../shared/shared.module';
import { ToolbarModule } from 'primeng/toolbar';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    WorkTaskListComponent,
    WorkTaskFormComponent,
  ],
  imports: [
    // Angular
    CommonModule,
    RouterModule.forChild([
      { path: 'list', component: WorkTaskListComponent },
      { path: 'form', component: WorkTaskFormComponent },
      { path: 'form/:id', component: WorkTaskFormComponent },
      { path: '**', redirectTo: 'list' },
    ]),
    FormsModule,
    ReactiveFormsModule,

    // Project
    SharedModule,

    // PrimeNg
    ButtonModule,
    InputTextModule,
    TableModule,
    ToolbarModule,
  ],
})
export class WorkTasksModule { }
