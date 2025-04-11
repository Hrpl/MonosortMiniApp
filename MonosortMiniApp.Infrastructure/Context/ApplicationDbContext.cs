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
    public DbSet<Additive> Milks { get; set; }
    public DbSet<TypeAdditive> Sirups { get; set; }
    public DbSet<Volume> Volumes { get; set; }
    public DbSet<Drink> Drinks { get; set; }
    public DbSet<Menu> Menu { get; set; }
    public DbSet<PriceDrink> PriceDrinks { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }
    public DbSet<OrderStatus> OrderStatus { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Connection> Connections { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
