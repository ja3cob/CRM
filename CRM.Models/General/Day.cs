using CRM.Models.Database;

namespace CRM.Models.General
{
    public class Day
    {
        public DateOnly Date { get; }
        public List<ToDoItem>? ToDoItems { get; set; }

        public Day(DateOnly date)
        {
            Date = date;
        }
    }
}
