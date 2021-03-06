import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { AgentRuntimeService } from '@app/features/agents';
import { SourceControlService } from '@app/features/source-control';
import { WorkspaceEventService } from '@app/features/workspace-events';
import { BehaviorSubject, combineLatest } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DashboardComponent implements OnInit {

  public currentDate: Date = new Date();
  public maxDate: Date = new Date();
  private currentDate$ = new BehaviorSubject<Date>(new Date());

  public vm$ = this.currentDate$.pipe(
    switchMap((date) => combineLatest([
      this.isMaxDate(date) ? this.agentRuntimeService.agentRuntimes$ : this.agentRuntimeService.get(date),
      this.isMaxDate(date) ? this.workspaceEventService.workspaceEvents$ : this.workspaceEventService.get(date),
      this.isMaxDate(date) ? this.sourceControlService.sourceControls$ : this.sourceControlService.get(date),
    ]).pipe(
      map(([agentRuntimes, workspaceEvents, sourceControls]) => ({
        agentRuntimes,
        workspaceEvents,
        sourceControls,
        isMaxDate: this.isMaxDate(date),
        selectedDate: date,
      })),
    )),
  );

  constructor(
    private agentRuntimeService: AgentRuntimeService,
    private workspaceEventService: WorkspaceEventService,
    private sourceControlService: SourceControlService,
  ) { }

  ngOnInit(): void {
  }

  public isMaxDate(date: Date): boolean {
    if (date.getFullYear() == this.maxDate.getFullYear()) {
      if (date.getMonth() == this.maxDate.getMonth()) {
        return date.getDate() >= this.maxDate.getDate();
      }
      else {
        return date.getMonth() > this.maxDate.getMonth();
      }
    }
    else {
      return date.getFullYear() > this.maxDate.getFullYear();
    }
  }

  public previousDay(): void {
    this.currentDate = new Date(this.currentDate.getTime() - 24 * 60 * 60 * 1000);
    this.currentDate$.next(this.currentDate);
  }

  public dateChanged(date: Date): void {
    this.currentDate$.next(date);
  }

  public nextDay(disabled: boolean): void {
    if (disabled) {
      return;
    }

    this.currentDate = new Date(this.currentDate.getTime() + 24 * 60 * 60 * 1000);
    this.currentDate$.next(this.currentDate);
  }
}
