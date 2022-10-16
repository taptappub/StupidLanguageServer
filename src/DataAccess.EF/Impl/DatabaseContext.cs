using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF.Impl;

class DatabaseContext : DbContext
{
    private readonly string _connectionString;
    public DatabaseContext(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException(nameof(connectionString));

        _connectionString = connectionString;
        var script = Database.GenerateCreateScript();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
