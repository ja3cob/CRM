using CRM.Models.Database;

namespace CRM.Services
{
    public class ToDoItemsServiceDebug : IToDoItemsService
    {
        public IEnumerable<ToDoItem> GetToDoItems(int year, int month)
        {
            var testPerson = new Person("test", "", null, null, Models.Role.User);
            var date = new DateOnly(year, month, 1);
            var startTime = new TimeOnly(10, 0);
            var endTime = startTime.AddHours(3);

            string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin volutpat, eros eget posuere venenatis, velit nisl ullamcorper tortor, non eleifend elit libero eu quam. Sed id facilisis nisi.";
            return new List<ToDoItem>
            {
                new ToDoItem(date, startTime, endTime, lorem, 0, testPerson, testPerson),
                new ToDoItem(date, startTime.AddHours(1), endTime.AddHours(1), lorem, 10, testPerson, testPerson),
                new ToDoItem(date, startTime.AddHours(2), endTime.AddHours(2), lorem, 20, testPerson, testPerson),
                new ToDoItem(date, startTime.AddHours(3), endTime.AddHours(3), lorem, 30, testPerson, testPerson),
                new ToDoItem(date.AddDays(1), startTime, endTime, lorem, 0, testPerson, testPerson),
                new ToDoItem(date.AddDays(1), startTime.AddHours(1), endTime.AddHours(1), lorem, 10, testPerson, testPerson),
                new ToDoItem(date.AddDays(1), startTime.AddHours(2), endTime.AddHours(2), lorem, 20, testPerson, testPerson),
                new ToDoItem(date.AddDays(1), startTime.AddHours(3), endTime.AddHours(3), lorem, 30, testPerson, testPerson),
            };
        }
    }
}