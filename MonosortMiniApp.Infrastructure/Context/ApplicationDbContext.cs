using Microsoft.EntityFrameworkCore;
using MonosortMiniApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Coffee> Coffees { get; set; }
    public DbSet<Limonades> Limonades { get; set; }
    public DbSet<Milk> Milks { get; set; }
    public DbSet<Sirup> Sirups { get; set; }
    public DbSet<Tea> Tea { get; set; }
    public DbSet<Volume> Volumes { get; set; }
    public DbSet<Dessert> Desserts { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}
