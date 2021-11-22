import { WorkspaceEventType } from "../enums";

export class WorkspaceEventDto {
    public id!: string;
    public type!: WorkspaceEventType;
    public from!: Date;
    public to?: Date;
    public agentId!: string;
    public data?: string;
}
