import { Tray, Menu, app, shell, MenuItemConstructorOptions } from 'electron';
import path from 'path';

export class TrayService {
    private static menuTemplate: MenuItemConstructorOptions[] = [
        {
            id: 'menu1',
            label: 'Menu1',
            click: () => { console.log('1') },
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
        // this.tray.setToolTip('Test tooltip' + i);
        this.updateTray();
    }

    private static updateTray(): void {
        setInterval(() => {
            if (this.tray == null) {
                return;
            }
    
            /*i++;
            this.menuTemplate[0].label = 'dupa' + i;
            let menu = Menu.buildFromTemplate(menuTemplate);
            this.tray.setContextMenu(menu);
            this.tray.setToolTip('Test tooltip' + i);*/
        }, 1000);
    }
}
