namespace CRM.Models.General
{
    public class Month
    {
        public DateTime CurrentDate { get; }
        public Day[] Days { get; }

        public Month(DateTime currentDate, Day[] days)
        {
            CurrentDate = currentDate;
            Days = days;
        }
    }
}