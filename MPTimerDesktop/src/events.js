const eventsDb = require('./events-db');

let allEvents;

function init() {
   allEvents = eventsDb.loadEvents();
   console.log(allEvents);
}

module.exports = {
    init,
}
