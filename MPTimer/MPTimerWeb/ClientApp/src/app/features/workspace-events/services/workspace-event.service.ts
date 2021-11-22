import { Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { combineLatest } from "rxjs";
import { map, startWith, switchMap } from "rxjs/operators";
import { SignalRService } from "@app/services";
import { HttpClient } from "@angular/common/http";
import { WorkspaceEventDto } from "../models";

@Injectable({ providedIn: 'root' })
export class WorkspaceEventService {
  private agentConnected$ = this.signalRService.on$("WorkspaceEventStarted").pipe(startWith(null));
  private agentDisconnected$ = this.signalRService.on$("WorkspaceEventStoped").pipe(startWith(null));

  public workspaceEvents$ = combineLatest([
    this.agentConnected$,
    this.agentDisconnected$
  ]).pipe(
    switchMap(() => this.httpClient.get<WorkspaceEventDto[]>(`${environment.backendUrl}/api/workspaceEvents`)),
    map((runtimes) => runtimes.map(runtime => ({
      ...runtime,
      from: new Date(runtime.from),
      to: runtime.to != null ? new Date(runtime.to) : null,
    }) as WorkspaceEventDto)),
  );
  
  constructor(
    private httpClient: HttpClient,
    private signalRService: SignalRService,
  ) {
    
  }
}
