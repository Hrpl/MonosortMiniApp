using Microsoft.EntityFrameworkCore;
using Form.Domain.Domain;

namespace Form.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<FormEntity> Forms { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }
}
