import { Component, OnInit } from '@angular/core';
import { ToastService } from '@app/services';
import { ConfirmationService } from 'primeng/api';
import { Observable } from 'rxjs';
import { AgentDto } from '../../models';
import { AgentService } from '../../services';
import { AgentTypeTranslator } from '../../utils';

@Component({
  selector: 'app-agents-list',
  templateUrl: './agents-list.component.html',
  styleUrls: ['./agents-list.component.scss']
})
export class AgentsListComponent implements OnInit {

  public agents$: Observable<AgentDto[]>;
  public loading$: Observable<boolean>;
  public AgentTypeTranslator = AgentTypeTranslator;
  
  constructor(
    private agentService: AgentService,
    private confirmationService: ConfirmationService,
    private toastService: ToastService,
  ) {
    this.agents$ = agentService.entities$;
    this.loading$ = agentService.loading$;
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
}
