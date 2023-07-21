using CRM.Models.Database;

namespace CRM.Models.General
{
    public class Day
    {
        public DateTime CurrentDate { get; }
        public ToDoItem[] ToDoItems { get; }

        public Day(DateTime currentDate, ToDoItem[] toDoItems)
        {
            CurrentDate = currentDate;
            ToDoItems = toDoItems;
        }
    }
}
