using CRM.Database;
using CRM.Models.Database;

namespace CRM.Services
{
    public class PeopleService 
    {
        private readonly CRMDbContext _dbContext;
        public PeopleService(CRMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Person Get(string username)
        {
            return _dbContext.People.First(x => x.Username == username);
        }
        public void Add(Person person)
        {
            _dbContext.People.Add(person);
            _dbContext.SaveChanges();
        }
    }
}

