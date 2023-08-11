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
		}

		public ToDoItem? Get(int id)
		{
			return _dbContext.ToDoItems.FirstOrDefault(x => x.Id == id);
		}
        public IEnumerable<ToDoItem> GetList(int year, int month)
        {
            var ret = _dbContext.ToDoItems.Include(p => p.CreatedBy).Where(p => p.Date.Year == year && p.Date.Month == month);
			return ret;
        }
		public void Save(ToDoItem toDoItem)
		{
			if (toDoItem.Id != null)
			{
				var oldItem = Get(toDoItem.Id.Value);
				if(oldItem != null)
				{
					_dbContext.Entry(oldItem).CurrentValues.SetValues(toDoItem);
                    _dbContext.SaveChanges();
					return;
                }
            }

			_dbContext.ToDoItems.Add(toDoItem);
			_dbContext.SaveChanges();
		}
		public void Delete(int id)
		{
			_dbContext.ToDoItems.Where(x => x.Id == id).ExecuteDelete();
		}
    }
}

