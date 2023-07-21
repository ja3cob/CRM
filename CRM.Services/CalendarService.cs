using CRM.Models.General;

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

            return new Month(days.ToArray(), month, year);
        }
    }
}