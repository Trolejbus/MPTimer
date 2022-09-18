import { EventModel } from '../models';
import { DateUtils } from '../utils';
import { EventsDbService } from './events-db.service';

export class EventsService {
    private static allEvents: EventModel[] = [];

    public static init(): void {
        this.allEvents = EventsDbService.loadEvents();
    }

    public static addEvent(event: EventModel): void {
        this.allEvents.push(event);
        EventsDbService.addEvent(event);
    }
}
