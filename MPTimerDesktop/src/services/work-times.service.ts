import { combineLatestWith, interval, map } from 'rxjs';
import { EventModel, WorkTimesModel } from '../models';
import { EventsService } from './events.service';

export class WorkTimesService {
    public static workTimes$ = EventsService.events$.pipe(
        combineLatestWith(interval(1000)),
        map(([events]) => this.countWorkTimes(events)),
    );

    public static init(): void {

    }

    private static countWorkTimes(events: EventModel[]): WorkTimesModel {
        return new WorkTimesModel(Math.random() * 3000, Math.random() * 3000, Math.random() * 3000);
    }
}