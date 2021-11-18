import { EntityMetadataMap, EntityDataModuleConfig } from '@ngrx/data';

const entityMetadata: EntityMetadataMap = {
  WorkTask: {},
  Agent: {},
  WorkspaceEvent: {},
};

const pluralNames = {  };

export const entityConfig: EntityDataModuleConfig = {
  entityMetadata,
  pluralNames
};
