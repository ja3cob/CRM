const daysInCalendar = 7 * 6;
const daysInWeek = 7;
const monday = 1;

function generateMonth(date: Date): Date[] {
    var days = [];
    var tempDay = new Date(date.getTime());
    days.push(new Date(tempDay.getTime()));

    while (days[0].getDay() != monday) {
        tempDay.addDays(-1)
        days.unshift(new Date(tempDay.getTime()));
    }
    tempDay = new Date(date.getTime());
    tempDay.addDays(1);

    while (days.length < daysInCalendar) {
        days.push(new Date(tempDay.getTime()));
        tempDay.addDays(1);
    }

    return days;
}