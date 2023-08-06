using CRM.Models.Database;

namespace CRM.Services
{
	public interface IToDoItemsService
	{
        public IEnumerable<ToDoItem> GetToDoItems(int year, int month);
    }
}

