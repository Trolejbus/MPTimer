import { Component, Input, OnInit } from '@angular/core';
import { AgentRuntimeDto, AgentRuntimeService, AgentService } from '@app/features/agents';
import { SourceControlService } from '@app/features/source-control';
import { WorkspaceEventDto, WorkspaceEventService, WorkspaceEventType } from '@app/features/workspace-events';
import { BehaviorSubject, combineLatest, Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { WorkTimeLineSectionModel } from './models';

@Component({
  selector: 'app-work-time-line',
  templateUrl: './work-time-line.component.html',
  styleUrls: ['./work-time-line.component.scss'],
})
export class WorkTimeLineComponent implements OnInit {

  private agentRuntimes$ = new BehaviorSubject<AgentRuntimeDto[]>([]);
  private workspaceEvents$ = new BehaviorSubject<WorkspaceEventDto[]>([]);

  @Input()
  public set agentRuntimes(value: AgentRuntimeDto[]) {
    this.agentRuntimes$.next(value);
  }

  @Input()
  public set workspaceEvents(value: WorkspaceEventDto[]) {
    this.workspaceEvents$.next(value);
  }

  public sections$: Observable<WorkTimeLineSectionModel[]> = combineLatest([
    this.agentService.entities$,
    this.agentRuntimes$,
    this.workspaceEvents$,
    this.sourceControlService.sourceControls$.pipe(startWith([])),
  ]).pipe(
    map(([agents, runtimes, workspaceEvents, sourceControls]) => ([
      {
        events: agents.map(agent => ({
          id: `${agent.id}_runtime`,
          name: agent.name,
          color: '#336B87',
          activities: runtimes.filter(runtime => runtime.agentId == agent.id).map(runtime => ({
            id: runtime.id,
            from: runtime.from,
            to: runtime.to,
          })),
        })),
      },
      {
        events: [
          ...agents.map(agent => ({
            id: `${agent.id}_lock-screen`,
            name: 'Lock screen',
            color: '#2A3132',
            activities: workspaceEvents
              .filter(event => event.type == WorkspaceEventType.ScreenLocked)
              .filter(event => event.agentId == agent.id).map(event => ({
                id: event.id,
                from: event.from,
                to: event.to,
                notes: `Reason: ${event.data}`,
              })),
          })),
          ...agents.map(agent => ({
            id: `${agent.id}_idle-time`,
            name: 'Idle time',
            color: '#A8A8A8',
            activities: workspaceEvents
              .filter(event => event.type == WorkspaceEventType.IdleTime)
              .filter(event => event.agentId == agent.id).map(event => ({
                id: event.id,
                from: event.from,
                to: event.to,
              })),
          })),
        ],
      },
      {
        events: [
          ...sourceControls.map(sourceControl => ({
            id: `${sourceControl.id}_source-control`,
            name: sourceControl.name,
            color: '#336B87',
            activities: sourceControl.statuses.map(status => ({
              id: status.id,
              from: status.from,
              to: status.to,
              notes: status.branchName,
            })),
          })),
        ]
      }
    ])),
  )

  constructor(
    private agentRuntimeService: AgentRuntimeService,
    private agentService: AgentService,
    private workspaceEventService: WorkspaceEventService,
    private sourceControlService: SourceControlService,
  ) { }

  ngOnInit(): void {
    this.agentService.getAll();
  }


}
