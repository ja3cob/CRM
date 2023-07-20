namespace CRM.Models
{
    public class Month
    {
        public int MonthNumber { get; }
        public int Year { get; }
        public Day[] Days { get; }

        public Month(Day[] days, int monthNumber, int year)
        {
            Days = days;
            MonthNumber = monthNumber;
            Year = year;
        }
    }
}