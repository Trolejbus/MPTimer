import { SourceControlStateDto } from "./source-control-state.dto";

export class SourceControlDto {
    public id!: string;
    public name!: string;
    public path!: string;
    public agentId!: string;
    public statuses!: SourceControlStateDto[];
}
