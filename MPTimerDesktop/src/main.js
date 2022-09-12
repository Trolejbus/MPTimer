const electron = require('electron');
const trayService = require('./tray.service');
const app = electron.app;

app.on('ready', _ => {
    trayService.init();
});

app.on('before-quit', _ => {
    console.log('quit');
});
