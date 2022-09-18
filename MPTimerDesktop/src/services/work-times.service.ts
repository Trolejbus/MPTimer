import { combineLatestWith, interval, map } from 'rxjs';
import { EventType } from '../enums';
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
        let runningTime = 0;
        let breakTime = 0;
        let previousRun: number | null = null;
        let previousLock: number | null = null;

        for (const event of events)
        {
            const eventTimeSpan = this.getTimeSpanInSecond(event.date);
            if (event.type === EventType.AppStarted)
            {
                previousRun = previousRun ?? eventTimeSpan;
            }

            if (event.type === EventType.UserLock)
            {
                previousLock = previousLock ?? eventTimeSpan;
            }

            if (event.type === EventType.UserUnlock)
            {
                previousRun = previousRun ?? eventTimeSpan;
                if (previousLock != null)
                {
                    breakTime += eventTimeSpan - previousLock;
                    previousLock = null;
                }
            }

            if (event.type === EventType.AppStopped)
            {
                if (previousRun != null)
                {
                    runningTime += eventTimeSpan - previousRun;
                    previousRun = null;
                }

                if (previousLock != null)
                {
                    breakTime += eventTimeSpan - previousLock;
                    previousLock = null;
                }
            }
        }

        if (previousRun != null)
        {
            runningTime += previousLock ?? this.getTimeSpanInSecond(new Date()) - previousRun;
        }

        if (previousLock != null)
        {
            breakTime += this.getTimeSpanInSecond(new Date()) - previousLock;
        }

        var workTime = runningTime - breakTime;
        return new WorkTimesModel(runningTime, breakTime, workTime);
    }

    private static getTimeSpanInSecond(date: Date): number {
        return date.getTime() / 1000;
    }
}