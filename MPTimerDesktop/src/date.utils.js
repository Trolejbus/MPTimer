
function formatDate(date) {
    if (date == null) {
        return null;
    }

    if (!(date instanceof Date)) {
        throw new Error('Argument "date" is not in Date type');
    }

    var result = `${date.getFullYear()}-${(date.getMonth() + 1).toString().padStart(2, '0')}-${date.getDate().toString().padStart(2, '0')}`;
    return result;
}

module.exports = {
    formatDate,
}
