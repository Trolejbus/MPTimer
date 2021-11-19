import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { EntityCollectionServiceBase, EntityCollectionServiceElementsFactory } from "@ngrx/data";
import { environment } from "../../../../environments/environment";
import { combineLatest } from "rxjs";
import { AgentDto, AgentStateDto } from "../models";
import { SignalRService } from "@app/services";
import { startWith, switchMap } from "rxjs/operators";

@Injectable({ providedIn: 'root' })
export class AgentService extends EntityCollectionServiceBase<AgentDto> {
  private agentConnected$ = this.signalRService.on$("AgentConnected").pipe(startWith(null));
  private agentDisconnected$ = this.signalRService.on$("AgentDisconnected").pipe(startWith(null));

  public agentStates$ = combineLatest([
    this.agentConnected$,
    this.agentDisconnected$
  ]).pipe(
    switchMap(() => this.httpClient.get<AgentStateDto[]>(`${environment.backendUrl}/api/agents/status`))
  );
  
  constructor(
    private httpClient: HttpClient,
    private signalRService: SignalRService,
    serviceElementsFactory: EntityCollectionServiceElementsFactory,
  ) {
    super('Agent', serviceElementsFactory);
    
  }
}