namespace CRM.Models.General
{
    public class Month
    {
        public DateOnly Date { get; }
        public Day[] Days { get; }

        public Month(DateOnly date, Day[] days)
        {
            Date = date;
            Days = days;
        }
    }
}