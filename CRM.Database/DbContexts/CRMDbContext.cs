using CRM.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CRM.Database.DbContexts;
public abstract class CRMDbContext : DbContext
{
    protected readonly string _connectionString;
    public CRMDbContext(IConfiguration configuration, string connectionStringName)
    {
        var connectionString = configuration.GetConnectionString(connectionStringName);
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception($"{connectionStringName} connection string was null");
        }
        _connectionString = connectionString;
    }

    public DbSet<Person> People { get; set; }
    public DbSet<ToDoItem> ToDoItems { get; set; }
}