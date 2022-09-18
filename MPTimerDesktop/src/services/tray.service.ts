import { Tray, Menu, app, shell, MenuItemConstructorOptions } from 'electron';
import path from 'path';
import { WorkTimesModel } from '../models';
import { DateUtils } from '../utils';
import { WorkTimesService } from './work-times.service';

export class TrayService {
    private static menuTemplate: MenuItemConstructorOptions[] = [
        {
            label: 'Running time: -',
        },
        {
            label: 'Break time: -',
        },
        {
            label: 'Work time: -',
        },
        {
            type: 'separator',
        },
        {
            label: 'Open in explorer',
            click: _ => shell.openPath(''),
        },
        {
            label: 'Quit',
            click: _ => app.quit(),
        },
    ];
    private static tray: Tray;

    public static init(): void {
        this.tray = new Tray(path.join('src', 'tray.ico'));
        let menu = Menu.buildFromTemplate(this.menuTemplate);
        this.tray.setContextMenu(menu);
        WorkTimesService.workTimes$.subscribe((workTimes) => {
            this.updateTray(workTimes);
        });
    }

    private static updateTray(workTimes: WorkTimesModel): void {
        this.menuTemplate[0].label = `Running time: ${DateUtils.formatTimeSpan(workTimes.runningTimeSeconds)}`;
        this.menuTemplate[1].label = `Break time: ${DateUtils.formatTimeSpan(workTimes.breakTimeSeconds)}`;
        this.menuTemplate[2].label = `Work time: ${DateUtils.formatTimeSpan(workTimes.workTimeSeconds)}`;
        let menu = Menu.buildFromTemplate(this.menuTemplate);
        this.tray.setContextMenu(menu);
        this.tray.setToolTip(this.getTooltipText(workTimes));
    }

    private static getTooltipText(workTimes: WorkTimesModel): string {
        return `MPTimer

Running time: ${DateUtils.formatTimeSpan(workTimes.runningTimeSeconds)}
Break time: ${DateUtils.formatTimeSpan(workTimes.breakTimeSeconds)}
Work time: ${DateUtils.formatTimeSpan(workTimes.workTimeSeconds)}`;
    }
}
