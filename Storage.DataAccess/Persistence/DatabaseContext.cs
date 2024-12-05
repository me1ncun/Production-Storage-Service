using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Storage.Core.Enitities;

namespace Storage.DataAccess.Persistence.Migrations;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<ProductionFacility> ProductionFacilities { get; set; }
}