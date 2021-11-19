import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { EntityCollectionServiceBase, EntityCollectionServiceElementsFactory } from "@ngrx/data";
import { environment } from "../../../../environments/environment";
import { Observable } from "rxjs";
import { AgentDto, AgentStateDto } from "../models";

@Injectable({ providedIn: 'root' })
export class AgentService extends EntityCollectionServiceBase<AgentDto> {
  constructor(private httpClient: HttpClient, serviceElementsFactory: EntityCollectionServiceElementsFactory) {
    super('Agent', serviceElementsFactory);
  }

  public getStates(): Observable<AgentStateDto[]> {
    return this.httpClient.get<AgentStateDto[]>(`${environment.backendUrl}/api/agents/status`);
  }
}