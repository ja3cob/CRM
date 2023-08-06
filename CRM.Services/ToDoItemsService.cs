using CRM.Database;
using CRM.Models.Database;

namespace CRM.Services
{
	public class ToDoItemsService : IToDoItemsService
	{
		private readonly DatabaseService _databaseService;
		public ToDoItemsService(DatabaseService databaseService)
		{
			_databaseService = databaseService;
		}

        public IEnumerable<ToDoItem> GetToDoItems(int year, int month)
        {
			return _databaseService.GetToDoItems(year, month);
        }
    }
}

