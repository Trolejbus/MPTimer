import { powerMonitor } from 'electron';
import { EventType } from '../enums';
import { EventModel } from '../models';
import { EventsService } from './events.service';

export class WorkspaceEventService {
  public static init(): void {
    powerMonitor.addListener('lock-screen', () => {
      EventsService.addEvent(new EventModel(EventType.UserLock));
    });
  
    powerMonitor.addListener('unlock-screen', () => {
      EventsService.addEvent(new EventModel(EventType.UserUnlock));
    });
  }
}
