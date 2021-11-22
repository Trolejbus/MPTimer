import { Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { combineLatest } from "rxjs";
import { map, startWith, switchMap } from "rxjs/operators";
import { AgentRuntimeDto } from "../models";
import { SignalRService } from "@app/services";
import { HttpClient } from "@angular/common/http";
import { DateUtils } from "@app/shared";

@Injectable({ providedIn: 'root' })
export class AgentRuntimeService {
  private agentConnected$ = this.signalRService.on$("AgentConnected").pipe(startWith(null));
  private agentDisconnected$ = this.signalRService.on$("AgentDisconnected").pipe(startWith(null));

  public agentRuntimes$ = combineLatest([
    this.agentConnected$,
    this.agentDisconnected$
  ]).pipe(
    switchMap(() => this.httpClient.get<AgentRuntimeDto[]>(`${environment.backendUrl}/api/agentRuntimes`)),
    map((runtimes) => runtimes.map(runtime => ({
      ...runtime,
      from: DateUtils.convertUTCDateToLocalDate(runtime.from),
      to: runtime.to != null ? DateUtils.convertUTCDateToLocalDate(runtime.to) : null,
    }) as AgentRuntimeDto)),
  );
  
  constructor(
    private httpClient: HttpClient,
    private signalRService: SignalRService,
  ) {
    
  }
}

