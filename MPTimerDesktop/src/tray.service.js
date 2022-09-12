const electron = require('electron');
const path = require('path');

const Tray = electron.Tray;
const Menu = electron.Menu;
const app = electron.app;

const menuTemplate = [
    {
        id: 'menu1',
        label: 'Menu1',
        click: _ = console.log('1'),
    },
    {
        label: 'Quit',
        click: _ => app.quit(),
    }
];
let i = 0;
let tray;

function updateTray() {
    setInterval(() => {
        if (tray == null) {
            return;
        }

        i++;
        menuTemplate[0].label = 'dupa' + i;
        let menu = Menu.buildFromTemplate(menuTemplate);
        tray.setContextMenu(menu);
        tray.setToolTip('Test tooltip' + i);
    }, 1000);
}

function init() {
    console.log('inited');
    tray = new Tray(path.join('src', 'tray.ico'));
    let menu = Menu.buildFromTemplate(menuTemplate);
    tray.setContextMenu(menu);
    tray.setToolTip('Test tooltip' + i);

    updateTray();
}

module.exports = {
    init,
}
