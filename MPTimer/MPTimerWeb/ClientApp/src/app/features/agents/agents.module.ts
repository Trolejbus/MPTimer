import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '@app/shared';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { ToolbarModule } from 'primeng/toolbar';
import { AgentFormComponent, AgentsListComponent } from './components';
import { DropdownModule } from 'primeng/dropdown';
import { TagModule } from 'primeng/tag';

@NgModule({
  declarations: [
    AgentsListComponent,
    AgentFormComponent,
  ],
  imports: [
    // Angular
    CommonModule,
    RouterModule.forChild([
      { path: 'list', component: AgentsListComponent },
      { path: 'form', component: AgentFormComponent },
      { path: 'form/:id', component: AgentFormComponent },
      { path: '**', redirectTo: 'list' },
    ]),
    FormsModule,
    ReactiveFormsModule,

    // Project
    SharedModule,

    // PrimeNg
    ButtonModule,
    InputTextModule,
    ToolbarModule,
    DropdownModule,
    TagModule,
  ],
})
export class AgentsModule { }
