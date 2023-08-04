const daysInCalendar = 7 * 6;
const daysInWeek = 7;
const monday = 1;

interface Date {
    addDays(days: number): void;
    toISOStringDate(): string;
}
Date.prototype.addDays = function (days) {
    this.setDate(this.getDate() + days);
}
Date.prototype.toISOStringDate = function () {
    const year = this.getFullYear();
    var month = this.getMonth() + 1;
    var day = this.getDate();

    if (day < 10) {
        day = '0' + day;
    }
    if (month < 10) {
        month = '0' + month;
    }

    return year + '-' + month + '-' + day;
}

function generateMonth(year: number, month: number): Date[] {
    var days = [];
    const firstDay = new Date(year + "-" + month + "-1");
    var tempDay = new Date(firstDay.getTime());
    days.push(new Date(tempDay.getTime()));

    while (days[0].getDay() != monday) {
        tempDay.addDays(-1)
        days.unshift(new Date(tempDay.getTime()));
    }
    tempDay = new Date(firstDay.getTime());
    tempDay.addDays(1);

    while (days.length < daysInCalendar) {
        days.push(new Date(tempDay.getTime()));
        tempDay.addDays(1);
    }

    return days;
}