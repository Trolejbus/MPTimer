import { AgentStatus, AgentType } from "@app/features";

export class AgentTranslator {
    public static type(agentType: AgentType): string {
        switch (agentType) {
            case AgentType.DesktopApp:
                return "Desktop Tray Worker";
            default:
                throw new Error("Missing mapping");
        }
    }

    public static status(agentType: AgentStatus): string {
        switch (agentType) {
            case AgentStatus.Offline:
                return "Offline";
                case AgentStatus.Online:
                return "Online";
            default:
                throw new Error("Missing mapping");
        }
    }

}
