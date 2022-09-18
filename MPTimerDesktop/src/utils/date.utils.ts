export class DateUtils {

    public static format(date: Date, config?: { includeTime: boolean }): string | null {
        if (date == null) {
            return null;
        }
    
        if (!(date instanceof Date)) {
            throw new Error('Argument "date" is not in Date type');
        }
    
        var result = `${date.getFullYear()}-${this.formatD(date.getMonth() + 1)}-${this.formatD(date.getDate())}`;
        if (config?.includeTime ?? false) {
            result = `${result} ${this.formatD(date.getHours())}:${this.formatD(date.getMinutes())}:${this.formatD(date.getSeconds())}`;
        }

        return result;
    }

    private static formatD(digits: number): string {
        return digits.toString().padStart(2, '0');
    }
}
