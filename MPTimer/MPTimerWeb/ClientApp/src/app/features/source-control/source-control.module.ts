import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '@app/shared';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { SourceControlFormComponent, SourceControlListComponent } from './components';
import { DropdownModule } from 'primeng/dropdown';
import { TagModule } from 'primeng/tag';

@NgModule({
  declarations: [
    SourceControlListComponent,
    SourceControlFormComponent,
  ],
  imports: [
    // Angular
    CommonModule,
    RouterModule.forChild([
      { path: 'list', component: SourceControlListComponent },
      { path: 'form', component: SourceControlFormComponent },
      { path: 'form/:id', component: SourceControlFormComponent },
      { path: '**', redirectTo: 'list' },
    ]),
    FormsModule,
    ReactiveFormsModule,

    // Project
    SharedModule,

    // PrimeNg
    ButtonModule,
    InputTextModule,
    DropdownModule,
    TableModule,
    ToolbarModule,
    TagModule,
  ],
})
export class SourceControlModule { }
