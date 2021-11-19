import { Component, OnInit } from '@angular/core';
import { AgentStatus } from '@app/features';
import { ToastService } from '@app/services';
import { ConfirmationService } from 'primeng/api';
import { combineLatest, of } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { AgentDto, AgentStateDto } from '../../models';
import { AgentService } from '../../services';
import { AgentTranslator } from '../../utils';

@Component({
  selector: 'app-agents-list',
  templateUrl: './agents-list.component.html',
  styleUrls: ['./agents-list.component.scss']
})
export class AgentsListComponent implements OnInit {

  public agents$ = this.agentService.entities$;
  public loading$ = this.agentService.loading$;
  public agentStates$ = this.agentService.agentStates$;
  public vm$ = combineLatest([
    this.agents$.pipe(startWith(null)),
    this.loading$,
    this.agentService.agentStates$,
  ]).pipe(
    map(([agents, loading, states]) => ({ agents, loading, states})),
  );
  
  public AgentTranslator = AgentTranslator;
  public AgentStatus = AgentStatus;
  
  constructor(
    private agentService: AgentService,
    private confirmationService: ConfirmationService,
    private toastService: ToastService,
  ) {
  }

  ngOnInit() {
    this.agentService.getAll();
  }

  public remove(agent: AgentDto): void {
    this.confirmationService.confirm({
      message: `Are you sure that you want to remove ${agent.name}?`,
      accept: () => {
        this.agentService.delete(agent)
          .subscribe(() => {
            this.toastService.success(`Agent ${agent.name} has been successfully removed.`, "Agent");
          });
      }
    });
  }

  public agentStates(states: AgentStateDto[] | null, agent: AgentDto): AgentStateDto | null {
    return states?.find(s => s.agentId === agent.id) ?? null;
  }
}
