using Microsoft.EntityFrameworkCore;

namespace CRM.Database;
public class CRMDbContext: DbContext
{
    public CRMDbContext(DbContextOptions<CRMDbContext> options) : base(options) { }
}