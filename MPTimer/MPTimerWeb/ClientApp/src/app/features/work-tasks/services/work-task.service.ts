import { Injectable } from "@angular/core";
import { EntityCollectionServiceBase, EntityCollectionServiceElementsFactory } from "@ngrx/data";
import { WorkTaskDto } from "../models";

@Injectable({ providedIn: 'root' })
export class WorkTaskService extends EntityCollectionServiceBase<WorkTaskDto> {
  constructor(serviceElementsFactory: EntityCollectionServiceElementsFactory) {
    super('WorkTask', serviceElementsFactory);
  }
}