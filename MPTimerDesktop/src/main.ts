import { app } from 'electron';
import { EventType } from './enums';
import { EventModel } from './models';
import { EventsService, EventsDbService, TrayService, WorkspaceEventService } from './services';

export default class Main {
    public static main() {
        app.on('ready', _ => {
            EventsDbService.init();
            EventsService.init();

            TrayService.init();
            WorkspaceEventService.init();

            EventsService.addEvent(new EventModel(EventType.AppStarted));
        });

        app.on('quit', _ => {
            EventsService.addEvent(new EventModel(EventType.AppStopped));
        });
    }
}