using CRM.Models.Database;

namespace CRM.Services.Interfaces
{
    public interface IToDoItemsService
    {
        public ToDoItem Get(int id);
        public IEnumerable<ToDoItem> GetList(int year, int month);
        public void Add(ToDoItem item);
    }
}

