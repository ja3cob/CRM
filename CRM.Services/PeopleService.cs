using System.Net;

using CRM.Database;
using CRM.Models;
using CRM.Utilities;
using Microsoft.EntityFrameworkCore;

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
        public void Add(Person person)
        {
            if(Get(person.Username) != null)
            {
                throw new RequestException("Username already exists", HttpStatusCode.BadRequest);
            }
            _dbContext.People.Add(person);
            _dbContext.SaveChanges();
        }
        public void Update(string username, Person person)
        {
            var oldPerson = Get(username);
            if (oldPerson == null)
            {
                throw new RequestException("User does not exist", HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(person.Username))
            {
                throw new RequestException("Username cannot be empty", HttpStatusCode.BadRequest);
            }

            if (person.Password == null || string.IsNullOrWhiteSpace(person.Password))
            {
                person.Password = oldPerson.Password;
            }
            person.Id = oldPerson.Id;
            _dbContext.Entry(oldPerson).CurrentValues.SetValues(person);
            _dbContext.SaveChanges();
        }
        public void Delete(string username)
        {
            _dbContext.People.Where(x => x.Username == username).ExecuteDelete();
        }
    }
}

