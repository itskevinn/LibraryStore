using System.IO;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Api
{
  public class PersistenceContextFactory : IDesignTimeDbContextFactory<PersistenceContext>
  {
    public PersistenceContext CreateDbContext(string[] args)
    {
      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

      var optionsBuilder = new DbContextOptionsBuilder<PersistenceContext>();
      optionsBuilder.UseSqlServer(config.GetConnectionString("database"),
        sqlOpts => { sqlOpts.MigrationsHistoryTable("_MigrationHistory", config.GetValue<string>("SchemaName")); });

      return new PersistenceContext(optionsBuilder.Options, config);
    }
  }
}