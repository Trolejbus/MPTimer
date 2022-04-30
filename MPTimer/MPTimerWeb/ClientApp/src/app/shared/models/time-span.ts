export class TimeSpan {
    public hour!: number;
    public minute!: number;
    public second!: number;

    constructor (hour: number, minute: number, second: number) {
        this.hour = hour;
        this.minute = minute;
        this.second = second;
    }

    public static createFromSeconds(seconds: number): TimeSpan {
        const totalSeconds = Math.floor(seconds);
        const hour = Math.floor(totalSeconds / 3600);
        const minute = Math.floor((totalSeconds - hour * 3600) / 60);
        const second = totalSeconds - hour * 3600 - minute * 60;
        return new TimeSpan(hour, minute, second);
    }

    public static empty(): TimeSpan {
        return new TimeSpan(0, 0, 0);
    }

    public minusDate(date: Date): Date {
        return new Date(date.getTime() - this.totalSeconds() * 1000);
    }

    public addDate(date: Date): Date {
        return new Date(date.getTime() + this.totalSeconds() * 1000);
    }

    public totalSeconds(): number {
        return this.hour * 3600 + this.minute * 60 + this.second;
    }

    public format(showSeconds = true): string {
        let result = this.hour < 10 ? `0${this.hour}` : this.hour.toString();
        result += ':' + (this.minute < 10 ? `0${this.minute}` : this.minute.toString());
        return result += showSeconds ? ':' + (this.second < 10 ? `0${this.second}` : this.second.toString()) : '';
    }
}
