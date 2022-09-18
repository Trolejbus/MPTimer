import { BehaviorSubject } from 'rxjs';
import { EventModel } from '../models';
import { EventsDbService } from './events-db.service';

export class EventsService {
    private static allEventsSubject$ = new BehaviorSubject<EventModel[]>([]);
    public static events$ = this.allEventsSubject$.asObservable();

    public static init(): void {
        this.allEventsSubject$.next(EventsDbService.loadEvents());
    }

    public static addEvent(event: EventModel): void {
        this.allEventsSubject$.next([...this.allEventsSubject$.value, event]);
        EventsDbService.addEvent(event);
    }
}
