export class DateUtils {
    public static convertUTCDateToLocalDate(dateString: Date | string): Date {
        if (dateString instanceof Date) {
            return dateString;
        }

        const now = new Date();
        var b = dateString.split(/\D+/)
            .slice(0, 7).map(c => Number(c));
        const date = new Date(Date.UTC(b[0], b[1] - 1, b[2], b[3], b[4], b[5], 0));
        return date;
    }

    public static format(date: Date): string {
        return `${
            (date.getMonth()+1).toString().padStart(2, '0')}/${
            date.getDate().toString().padStart(2, '0')}/${
            date.getFullYear().toString().padStart(4, '0')} ${
            date.getHours().toString().padStart(2, '0')}:${
            date.getMinutes().toString().padStart(2, '0')}:${
            date.getSeconds().toString().padStart(2, '0')}`;
    }

    public static isInCurrentDay(date: Date): boolean {
        const maxDate = new Date();
        if (date.getFullYear() == maxDate.getFullYear()) {
            if (date.getMonth() == maxDate.getMonth()) {
                return date.getDay() >= maxDate.getDay();
            }
            else {
                return date.getMonth() > maxDate.getMonth();
            }
        }
        else {
            return date.getFullYear() > maxDate.getFullYear();
        }
    }
}