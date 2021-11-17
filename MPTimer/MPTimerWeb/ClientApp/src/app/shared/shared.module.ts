import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmptyMessageComponent, TableEmptyMessageComponent, TableSkeletonComponent } from './components';
import { SkeletonModule } from 'primeng/skeleton';

@NgModule({
  declarations: [
    EmptyMessageComponent,
    TableEmptyMessageComponent,
    TableSkeletonComponent,
  ],
  imports: [
    CommonModule,
    SkeletonModule,
  ],
  exports: [
    EmptyMessageComponent,
    TableEmptyMessageComponent,
    TableSkeletonComponent,
  ],
})
export class SharedModule { }
