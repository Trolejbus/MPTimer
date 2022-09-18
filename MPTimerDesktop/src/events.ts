import { EventsDb } from './events-db';

export class Events {
    private static allEvents: any[] = [];

    public static init(): void {
        this.allEvents = EventsDb.loadEvents();
        console.log(this.allEvents);
    }
}
