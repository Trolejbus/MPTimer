import { Injectable } from "@angular/core";
import { EntityCollectionServiceBase, EntityCollectionServiceElementsFactory } from "@ngrx/data";
import { AgentDto } from "../models";

@Injectable({ providedIn: 'root' })
export class AgentService extends EntityCollectionServiceBase<AgentDto> {
  constructor(serviceElementsFactory: EntityCollectionServiceElementsFactory) {
    super('Agent', serviceElementsFactory);
  }
}