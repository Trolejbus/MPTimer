import { AgentType } from "../enums";

export class AgentDto {
    public id!: string;
    public name!: string;
    public type!: AgentType;
}