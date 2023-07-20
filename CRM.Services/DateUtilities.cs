namespace CRM.Services
{
    internal static class DateUtilities
    {
        public static bool IsLastDayOfMonth(this DateTime date)
        {
            if(date.Day == DateTime.DaysInMonth(date.Year, date.Month))
            {
                return true;
            }
            return false;
        }
    }
}
