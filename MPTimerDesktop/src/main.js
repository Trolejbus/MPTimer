const electron = require('electron');
const trayService = require('./tray.service');
const workspaceEventService = require('./workspace-events.service');
const events = require('./events');
const eventsDb = require('./events-db');

const app = electron.app;

app.on('ready', _ => {
    eventsDb.init();
    events.init();

    trayService.init();
    workspaceEventService.init();
});

app.on('before-quit', _ => {
    console.log('quit');
});
