namespace CRM.Models
{
    public class Day
    {
        public DateTime CurrentDate { get; }

        public Day(DateTime currentDate)
        {
            CurrentDate = currentDate;
        }
    }
}
