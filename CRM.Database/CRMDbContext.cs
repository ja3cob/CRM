using CRM.Database.Converters;
using CRM.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CRM.Database;
public class CRMDbContext : DbContext
{
    public CRMDbContext(DbContextOptions options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoItem>(builder =>
        {
            // Date is a DateOnly property and date on database
            builder.Property(x => x.Date)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            // Time is a TimeOnly property and time on database
            builder.Property(x => x.StartTime)
                .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();
            builder.Property(x => x.EndTime)
                .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();
        });
        modelBuilder.Entity<ToDoItem>()
            .HasOne(x => x.CreatedBy);
        modelBuilder.Entity<ToDoItem>()
            .HasOne(x => x.AssignedTo);
    }
    public DbSet<Person> People { get; set; }
    public DbSet<ToDoItem> ToDoItems { get; set; }
}