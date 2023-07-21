using CRM.Models.Database;

namespace CRM.Models.General
{
    public class Day
    {
        public DateTime CurrentDate { get; }
        public List<ToDoItem>? ToDoItems { get; set; }

        public Day(DateTime currentDate)
        {
            CurrentDate = currentDate;
        }
    }
}
