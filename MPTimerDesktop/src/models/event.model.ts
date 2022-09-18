import { EventType } from '../enums';

export class EventModel {
    constructor (public type: EventType, public date: Date = new Date()) {
        
    }
}
