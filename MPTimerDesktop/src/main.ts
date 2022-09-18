import { app } from "electron";
import { Events } from "./events";
import { EventsDb } from "./events-db";
import { TrayService } from "./tray.service";
import { WorkspaceEventService } from "./workspace-event.service";

export default class Main {
    public static main() {
        app.on('ready', _ => {
            EventsDb.init();
            Events.init();

            TrayService.init();
            WorkspaceEventService.init();
        });

        app.on('before-quit', _ => {
            console.log('quit');
        });
    }
}