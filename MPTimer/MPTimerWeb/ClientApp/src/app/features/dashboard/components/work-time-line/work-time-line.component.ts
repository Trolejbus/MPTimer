import { Component, Input, OnInit } from '@angular/core';
import { AgentRuntimeService, AgentService } from '@app/features/agents';
import { WorkspaceEventService, WorkspaceEventType } from '@app/features/workspace-events';
import { combineLatest, Observable } from 'rxjs';
import { map, startWith, tap } from 'rxjs/operators';
import { WorkTimeLineSectionModel } from './models';

@Component({
  selector: 'app-work-time-line',
  templateUrl: './work-time-line.component.html',
  styleUrls: ['./work-time-line.component.scss'],
})
export class WorkTimeLineComponent implements OnInit {

  public sections$: Observable<WorkTimeLineSectionModel[]> = combineLatest([
    this.agentService.entities$,
    this.agentRuntimeService.agentRuntimes$.pipe(startWith([])),
    this.workspaceEventService.workspaceEvents$.pipe(startWith([])),
  ]).pipe(
    map(([agents, runtimes, workspaceEvents]) => ([
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
        events: agents.map(agent => ({
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
      },
    ])),
  )

  constructor(
    private agentRuntimeService: AgentRuntimeService,
    private agentService: AgentService,
    private workspaceEventService: WorkspaceEventService,
  ) { }

  ngOnInit(): void {
    this.agentService.getAll();
  }


}
