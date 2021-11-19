import { AgentStatus } from "../enums";

export class AgentStateDto {
    public agentId!: string;
    public status!: AgentStatus;
}
