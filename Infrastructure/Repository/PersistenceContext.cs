using System;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository
{
  public class PersistenceContext : DbContext
  {
    private readonly IConfiguration _config;

    public PersistenceContext(DbContextOptions<PersistenceContext> options, IConfiguration config) : base(options)
    {
      _config = config;
    }

    public async Task CommitAsync()
    {
      await SaveChangesAsync().ConfigureAwait(false);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema(_config.GetValue<string>("Store"));
      modelBuilder.Entity<Author>();
      modelBuilder.Entity<Book>();
      modelBuilder.Entity<Genre>();
      modelBuilder.Entity<Publisher>();


      foreach (var entityType in modelBuilder.Model.GetEntityTypes())
      {
        var t = entityType.ClrType;
        if (!typeof(DomainEntity).IsAssignableFrom(t)) continue;
        modelBuilder.Entity(entityType.Name).Property<DateTime>("CreatedOn").HasDefaultValueSql("GETDATE()");
        modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModifiedOn").HasDefaultValueSql("GETDATE()");
      }

      base.OnModelCreating(modelBuilder);
    }
  }
}