using CRM.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CRM.Database.DbContexts;
public class CRMDbContext : DbContext
{
    public CRMDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Person> People { get; set; }
    public DbSet<ToDoItem> ToDoItems { get; set; }
}