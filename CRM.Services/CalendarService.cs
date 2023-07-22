using CRM.Models.General;
using CRM.Models.Database;

namespace CRM.Services
{
    public class CalendarService
    {
        private const int DaysInCalendar = 7 * 6;
        public static Month GenerateMonth(int month, int year)
        {
            var days = new List<Day>();

            var tempDay = new DateTime(year, month, 1);

            for(int i = 0; tempDay.AddDays(-i).DayOfWeek != DayOfWeek.Sunday; i++) 
            {
                days.Insert(0, new Day(tempDay.AddDays(-i)));
            }
            tempDay = tempDay.AddDays(1);

            while(days.Count < DaysInCalendar)
            {
                days.Add(new Day(tempDay));
                tempDay = tempDay.AddDays(1);
            }

            var testPerson = new Person("test", "test", null, null, Models.Role.User);
            var startTime = new DateTime(days[4].CurrentDate.Year, days[4].CurrentDate.Month, days[4].CurrentDate.Day, 10, 0, 0);
            var endTime = startTime.AddHours(3);

            string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin volutpat, eros eget posuere venenatis, velit nisl ullamcorper tortor, non eleifend elit libero eu quam. Sed id facilisis nisi.";
            days[4].ToDoItems = new List<ToDoItem>
            {
                new ToDoItem(startTime, endTime, lorem, 0, testPerson, testPerson),
                new ToDoItem(startTime, endTime, lorem, 0, testPerson, testPerson),
                new ToDoItem(startTime, endTime, lorem, 0, testPerson, testPerson),
                new ToDoItem(startTime, endTime, lorem, 0, testPerson, testPerson),
            };
            days[2].ToDoItems = new List<ToDoItem>
            {
                new ToDoItem(startTime, endTime, lorem, 0, testPerson, testPerson),
                new ToDoItem(startTime, endTime, lorem, 0, testPerson, testPerson),
                new ToDoItem(startTime, endTime, lorem, 0, testPerson, testPerson),
                new ToDoItem(startTime, endTime, lorem, 0, testPerson, testPerson),
            };

            return new Month(days.ToArray(), month, year);
        }
    }
}