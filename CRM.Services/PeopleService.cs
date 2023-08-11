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

        public Person? Get(string username)
        {
            return _dbContext.People.FirstOrDefault(x => x.Username == username);
        }
        public IEnumerable<Person> GetList() 
        {
            return _dbContext.People;
        }
        public void Save(Person person)
        {
            if (string.IsNullOrWhiteSpace(person.Username))
            {
                var oldPerson = Get(person.Username);
                if (oldPerson != null)
                {
                    _dbContext.Entry(oldPerson).CurrentValues.SetValues(person);
                    _dbContext.SaveChanges();
                    return;
                }
            }

            _dbContext.People.Add(person);
            _dbContext.SaveChanges();
        }
    }
}

