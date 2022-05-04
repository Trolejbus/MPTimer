import { Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { combineLatest, Observable } from "rxjs";
import { map, startWith, switchMap } from "rxjs/operators";
import { SignalRService } from "@app/services";
import { HttpClient, HttpParams } from "@angular/common/http";
import { WorkspaceEventDto } from "../models";
import { DateUtils } from "@app/shared";

@Injectable({ providedIn: 'root' })
export class WorkspaceEventService {
  private agentConnected$ = this.signalRService.on$("WorkspaceEventAdded").pipe(startWith(null));
  private agentDisconnected$ = this.signalRService.on$("WorkspaceEventUpdated").pipe(startWith(null));

  public workspaceEvents$ = combineLatest([
    this.agentConnected$,
    this.agentDisconnected$
  ]).pipe(
    map(() => this.getFilterParams(new Date())),
    switchMap((filter) => this.httpClient.get<WorkspaceEventDto[]>(`${environment.backendUrl}/api/workspaceEvents`, { params: filter })),
    map((runtimes) => this.parseDates(runtimes)),
  );
  
  constructor(
    private httpClient: HttpClient,
    private signalRService: SignalRService,
  ) {
    
  }

  public get(date: Date): Observable<WorkspaceEventDto[]> {
    const filter = this.getFilterParams(date);
    return this.httpClient.get<WorkspaceEventDto[]>(`${environment.backendUrl}/api/workspaceEvents`, { params: filter }).pipe(
      map(result => this.parseDates(result)),
    );
  }

  private getFilterParams(date: Date): HttpParams {
    const from = new Date(new Date(date.getFullYear(), date.getMonth(), date.getDate()).getTime() + 1000 * 60 * 60 * 24);
    const to = new Date(new Date(date.getFullYear(), date.getMonth(), date.getDate()).getTime() + 2 * 1000 * 60 * 60 * 24);
    let params = new HttpParams();
    params = params.append('from', from.toISOString());
    params = params.append('to', to.toISOString());
    return params;
  }

  private parseDates(runtimes: WorkspaceEventDto[]): WorkspaceEventDto[] {
    return runtimes.map(runtime => ({
      ...runtime,
      from: DateUtils.convertUTCDateToLocalDate(runtime.from),
      to: runtime.to != null ? DateUtils.convertUTCDateToLocalDate(runtime.to) : null,
    }) as WorkspaceEventDto);
  }
}
