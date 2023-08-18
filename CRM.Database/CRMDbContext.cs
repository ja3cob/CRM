using CRM.Models;
using CRM.Utilities;
using Microsoft.EntityFrameworkCore;

namespace CRM.Database;
public class CRMDbContext : DbContext
{
    public CRMDbContext(DbContextOptions options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure conversions
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
            .Property(x => x.Id).IsRequired();

        modelBuilder.Entity<ToDoItem>()
            .HasOne(x => x.CreatedBy)
            .WithMany()
            .IsRequired();
        modelBuilder.Entity<ToDoItem>()
            .HasOne(x => x.AssignedTo);


        modelBuilder.Entity<Person>()
            .Property(x => x.Id).IsRequired();
        modelBuilder.Entity<Person>()
            .Property(x => x.Password).IsRequired();
        modelBuilder.Entity<Person>()
            .HasIndex(x => x.Username).IsUnique();
    }
    public DbSet<Person> People { get; set; }
    public DbSet<ToDoItem> ToDoItems { get; set; }
}