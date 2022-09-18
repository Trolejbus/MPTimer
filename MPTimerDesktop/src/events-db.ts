import { DateUtils } from './date.utils';
import fs from 'fs';
import path from 'path';

export class EventsDb {
    public static init() {
   
    }
    
    public static addEvent(event: any): void {
        const date = DateUtils.formatDate(new Date());
        const filePath = path.join('app-data', `${date}.txt`);
        if (!fs.existsSync(filePath)) {
            this.createFile(filePath);
        }
    }
    
    public static loadEvents(): any[] {
        const date = DateUtils.formatDate(new Date());
        const filePath = path.join('app-data', `${date}.txt`);
        if (!fs.existsSync(filePath)) {
            this.createFile(filePath);
        }
    
        const data = fs.readFileSync(filePath, 'utf-8');
        console.log(data);
        return [];
    }
    
    private static createFile(filePath: string): void {
        fs.writeFileSync(filePath, '');
    }
    
}
