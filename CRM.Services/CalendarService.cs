using CRM.Models.General;
using CRM.Models.Database;

namespace CRM.Services
{
    public class CalendarService
    {
        private const int DaysInCalendar = 7 * 6;
        public static Month GenerateMonth(int year, int month)
        {
            var days = new List<Day>();

            var tempDay = new DateOnly(year, month, 1);
            days.Add(new Day(tempDay));

            for (int i = -1; days[0].Date.DayOfWeek != DayOfWeek.Monday; i--)
            {
                days.Insert(0, new Day(tempDay.AddDays(i)));
            }
            tempDay = tempDay.AddDays(1);

            while (days.Count < DaysInCalendar)
            {
                days.Add(new Day(tempDay));
                tempDay = tempDay.AddDays(1);
            }

            var testPerson = new Person("test", "test", null, null, Models.Role.User);
            var startTime = new TimeOnly(10, 0);
            var endTime = startTime.AddHours(3);

            string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin volutpat, eros eget posuere venenatis, velit nisl ullamcorper tortor, non eleifend elit libero eu quam. Sed id facilisis nisi.";
            days[9].ToDoItems = new List<ToDoItem>
            {
                new ToDoItem(days[9].Date, startTime, endTime, lorem, 0, testPerson, testPerson),
                new ToDoItem(days[9].Date, startTime.AddHours(1), endTime.AddHours(1), lorem, 10, testPerson, testPerson),
                new ToDoItem(days[9].Date, startTime.AddHours(2), endTime.AddHours(2), lorem, 20, testPerson, testPerson),
                new ToDoItem(days[9].Date, startTime.AddHours(3), endTime.AddHours(3), lorem, 30, testPerson, testPerson),
            };
            days[11].ToDoItems = new List<ToDoItem>
            {
                new ToDoItem(days[11].Date, startTime, endTime, lorem, 0, testPerson, testPerson),
                new ToDoItem(days[11].Date, startTime.AddHours(1), endTime.AddHours(1), lorem, 10, testPerson, testPerson),
                new ToDoItem(days[11].Date, startTime.AddHours(2), endTime.AddHours(2), lorem, 20, testPerson, testPerson),
                new ToDoItem(days[11].Date, startTime.AddHours(3), endTime.AddHours(3), lorem, 30, testPerson, testPerson),
            };

            return new Month(new DateOnly(year, month, 1), days.ToArray());
        }
    }
}