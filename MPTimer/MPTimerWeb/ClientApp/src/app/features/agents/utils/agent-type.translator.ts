import { AgentType } from "@app/features";

export class AgentTypeTranslator {
    public static translate(agentType: AgentType): string {
        switch (agentType) {
            case AgentType.DesktopApp:
                return "Desktop Tray Worker";
            default:
                throw new Error("Missing mapping");
        }
    }
}
