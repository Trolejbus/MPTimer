import { Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { combineLatest, Observable } from "rxjs";
import { map, startWith, switchMap } from "rxjs/operators";
import { AgentRuntimeDto } from "../models";
import { SignalRService } from "@app/services";
import { HttpClient, HttpParams } from "@angular/common/http";
import { DateUtils } from "@app/shared";

@Injectable({ providedIn: 'root' })
export class AgentRuntimeService {
  private agentConnected$ = this.signalRService.on$("AgentConnected").pipe(startWith(null));
  private agentDisconnected$ = this.signalRService.on$("AgentDisconnected").pipe(startWith(null));

  public agentRuntimes$ = combineLatest([
    this.agentConnected$,
    this.agentDisconnected$
  ]).pipe(
    map(() => this.getFilterParams(new Date())),
    switchMap((filter) => this.httpClient.get<AgentRuntimeDto[]>(`${environment.backendUrl}/api/agentRuntimes`, { params: filter })),
    map((runtimes) => this.parseDates(runtimes)),
  );
  
  constructor(
    private httpClient: HttpClient,
    private signalRService: SignalRService,
  ) {
    
  }

  public get(date: Date): Observable<AgentRuntimeDto[]> {
    const filter = this.getFilterParams(date);
    return this.httpClient.get<AgentRuntimeDto[]>(`${environment.backendUrl}/api/agentRuntimes`, { params: filter }).pipe(
      map(result => this.parseDates(result)),
    );
  }

  private getFilterParams(date: Date): HttpParams {
    const from = new Date(new Date(date.getFullYear(), date.getMonth(), date.getDate()).getTime());
    const to = new Date(new Date(date.getFullYear(), date.getMonth(), date.getDate()).getTime() + 1000 * 60 * 60 * 24);
    let params = new HttpParams();
    params = params.append('from', from.toISOString());
    params = params.append('to', to.toISOString());
    return params;
  }

  private parseDates(runtimes: AgentRuntimeDto[]): AgentRuntimeDto[] {
    return runtimes.map(runtime => ({
      ...runtime,
      from: DateUtils.convertUTCDateToLocalDate(runtime.from),
      to: runtime.to != null ? DateUtils.convertUTCDateToLocalDate(runtime.to) : null,
    }) as AgentRuntimeDto);
  }
}

