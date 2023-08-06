using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CRM.Database.DbContexts
{
	public class MySqlDbContext : CRMDbContext
	{
        public MySqlDbContext(IConfiguration configuration) : base(configuration, "MySql") { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }
    }
}

