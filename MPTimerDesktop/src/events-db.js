const fs = require('fs');
const path = require('path');
const dateUtils = require('./date.utils');

function init() {
   
}

function addEvent(event) {
    const date = dateUtils.formatDate(new Date());
    const filePath = path.join('app-data', `${date}.txt`);
    if (!fs.existsSync(filePath)) {
        createFile(filePath);
    }
}

function loadEvents() {
    const date = dateUtils.formatDate(new Date());
    const filePath = path.join('app-data', `${date}.txt`);
    if (!fs.existsSync(filePath)) {
        createFile(filePath);
    }

    const data = fs.readFileSync(filePath, 'utf-8');
    console.log(data);
}

function createFile(filePath) {
    fs.writeFileSync(filePath, '');
}

module.exports = {
    init,
    loadEvents,
    addEvent,
}
