using CRM.Models.Database;

namespace CRM.Services
{
    public class ToDoItemsServiceDebug
    {
        public ToDoItem Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ToDoItem> GetList(int year, int month)
        {
            var testPerson = new Person { Username = "test", Role = Models.Role.User };
            var date = new DateOnly(year, month, 1);
            var startTime = new TimeOnly(10, 0);
            var endTime = startTime.AddHours(3);

            string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin volutpat, eros eget posuere venenatis, velit nisl ullamcorper tortor, non eleifend elit libero eu quam. Sed id facilisis nisi.";
            return new List<ToDoItem>
            {
                new ToDoItem{Date = date,            StartTime = startTime            , EndTime = endTime, Text = lorem, Progress =  0, AssignedTo = testPerson, CreatedBy = testPerson },
                new ToDoItem{Date = date,            StartTime = startTime.AddHours(1), EndTime = endTime, Text = lorem, Progress = 10, AssignedTo = testPerson, CreatedBy = testPerson },
                new ToDoItem{Date = date,            StartTime = startTime.AddHours(2), EndTime = endTime, Text = lorem, Progress = 20, AssignedTo = testPerson, CreatedBy = testPerson },
                new ToDoItem{Date = date,            StartTime = startTime.AddHours(3), EndTime = endTime, Text = lorem, Progress = 30, AssignedTo = testPerson, CreatedBy = testPerson },

                new ToDoItem{Date = date.AddDays(1), StartTime = startTime            , EndTime = endTime, Text = lorem, Progress =  0, AssignedTo = testPerson, CreatedBy = testPerson },
                new ToDoItem{Date = date.AddDays(1), StartTime = startTime.AddHours(1), EndTime = endTime, Text = lorem, Progress = 10, AssignedTo = testPerson, CreatedBy = testPerson },
                new ToDoItem{Date = date.AddDays(1), StartTime = startTime.AddHours(2), EndTime = endTime, Text = lorem, Progress = 20, AssignedTo = testPerson, CreatedBy = testPerson },
                new ToDoItem{Date = date.AddDays(1), StartTime = startTime.AddHours(3), EndTime = endTime, Text = lorem, Progress = 30, AssignedTo = testPerson, CreatedBy = testPerson }
            };
        }

        public void Save(ToDoItem item)
        {
            throw new NotImplementedException();
        }
    }
}