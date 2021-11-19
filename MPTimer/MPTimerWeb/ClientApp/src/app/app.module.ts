import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { FooterComponent, HeaderComponent, SidebarComponent } from './components';
import { StoreModule } from '@ngrx/store';
import { DefaultDataServiceConfig, EntityDataModule } from '@ngrx/data';
import { entityConfig } from './entity-metadata';
import { EffectsModule } from '@ngrx/effects';
import { environment } from '../environments/environment';
import { ToastModule } from 'primeng/toast';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { TooltipModule } from 'primeng/tooltip';

const defaultDataServiceConfig: DefaultDataServiceConfig = {
  root: `${environment.backendUrl}/api`,
  timeout: 3000, // request timeout
}

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    SidebarComponent,
    FooterComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'dashboard', loadChildren: () => import('./features/dashboard/dashboard.module').then(m => m.DashboardModule) },
      { path: 'work-tasks', loadChildren: () => import('./features/work-tasks/work-tasks.module').then(m => m.WorkTasksModule) },
      { path: 'agents', loadChildren: () => import('./features/agents/agents.module').then(m => m.AgentsModule) },
      { path: 'workspace-events', loadChildren: () => import('./features/workspace-events').then(m => m.WorkspaceEventsModule) },
      { path: '**', redirectTo: 'dashboard' },
    ]),
    StoreModule.forRoot({}, {}),
    EffectsModule.forRoot([]),
    EntityDataModule.forRoot(entityConfig),

    ToastModule,
    ConfirmDialogModule,
  ],
  providers: [
    { provide: DefaultDataServiceConfig, useValue: defaultDataServiceConfig },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
