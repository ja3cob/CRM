using CRM.Database.DbContexts;
using CRM.Models.Database;

namespace CRM.Database
{
	public class DatabaseService
	{
		private readonly CRMDbContext _dbContext;
		public DatabaseService(CRMDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IEnumerable<ToDoItem> GetToDoItems(int year, int month)
		{
			return _dbContext.ToDoItems.Where(p => p.Date == new DateOnly(year, month, 1));
		}
	}
}

