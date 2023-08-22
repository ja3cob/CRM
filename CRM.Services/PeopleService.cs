﻿using System.Net;

using CRM.Database;
using CRM.Models;
using CRM.Utilities;

namespace CRM.Services
{
    public class PeopleService
    {
        private readonly CRMDbContext _dbContext;
        public PeopleService(CRMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Person? Get(int id)
        {
            return _dbContext.People.FirstOrDefault(x => x.Id == id);
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
            if (Get(person.Username) != null)
            {
                throw new RequestException("Username already exists", HttpStatusCode.BadRequest);
            }
            _dbContext.People.Add(person);
            _dbContext.SaveChanges();
        }
        public void Update(int id, Person person)
        {
            var oldPerson = Get(id);
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
        public void Delete(int id)
        {
            if (_dbContext.ToDoItems.Any(x => x.CreatedById == id))
            {
                throw new RequestException("Cannot delete - there are tasks created by this person", HttpStatusCode.BadRequest);
            }
            var person = Get(id);
            if (person == null)
            {
                throw new RequestException("Person with such id does not exist", HttpStatusCode.BadRequest);
            }

            foreach (var toDoItem in _dbContext.ToDoItems.Where(x => x.AssignedToId == id))
            {
                toDoItem.AssignedToId = null;
            }
            _dbContext.People.Remove(person);
            _dbContext.SaveChanges();
        }
    }
}