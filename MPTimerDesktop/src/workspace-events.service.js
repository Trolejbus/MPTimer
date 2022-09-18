const { powerMonitor } = require('electron');
const events = require('./events-db');

function init() {
  console.log('init');
  powerMonitor.addListener('lock-screen', () => {
    console.log('lock');
    // Screen is locked, do something
  });

  powerMonitor.addListener('unlock-screen', () => {
    console.log('unlock');
    // Screen is unlocked, do something else
  });
}

module.exports = {
  init,
}
