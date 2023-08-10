using CRM.Database;
using CRM.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services
{
    public class ToDoItemsService
	{
		private readonly CRMDbContext _dbContext;
		public ToDoItemsService(CRMDbContext dbContext)
		{
            _dbContext = dbContext;
			_dbContext.Database.EnsureCreated();
		}

		public ToDoItem? Get(int id)
		{
			return _dbContext.ToDoItems.First(x => x.Id == id);
		}
        public IEnumerable<ToDoItem> GetList(int year, int month)
        {
			var ret = _dbContext.ToDoItems.Include(p => p.CreatedBy).Where(p => p.Date == new DateOnly(year, month, 1));
			return ret;
        }
		public void Add(ToDoItem toDoItem)
		{
			_dbContext.ToDoItems.Add(toDoItem);
			_dbContext.SaveChanges();
		}
    }
}

