import { app } from 'electron';
import { Events, EventsDb, TrayService, WorkspaceEventService } from './services';

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