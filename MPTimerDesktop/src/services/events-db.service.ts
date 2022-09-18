import { DateUtils } from '../utils';
import fs from 'fs';
import path from 'path';
import { EventModel } from '../models';
import { EventType } from '../enums';

export class EventsDbService {
    public static init() {
   
    }
    
    public static addEvent(event: EventModel): void {
        const date = DateUtils.format(new Date());
        const filePath = path.join('app-data', `${date}.txt`);
        if (!fs.existsSync(filePath)) {
            this.createFile(filePath);
        }

        fs.appendFileSync(filePath, `${EventType[event.type]}|${DateUtils.format(event.date, { includeTime: true })}\n`);
    }
    
    public static loadEvents(): EventModel[] {
        const date = DateUtils.format(new Date());
        const filePath = path.join('app-data', `${date}.txt`);
        if (!fs.existsSync(filePath)) {
            this.createFile(filePath);
        }
    
        const data = fs.readFileSync(filePath, 'utf-8'); 
        return data
            .split('\n')
            .filter(l => l !== '')
            .map(line => {
                const [typeRaw, dateRaw] = line.split('|');
                return new EventModel(EventType[typeRaw as any] as any, new Date(dateRaw));
            });
    }
    
    private static createFile(filePath: string): void {
        fs.writeFileSync(filePath, '');
    }
    
}
