using System.Data;
using Domain.Repository;
using Infrastructure.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
  public static class PersistenceExtensions
  {
    public static IServiceCollection AddPersistence(this IServiceCollection svc, IConfiguration config)
    {
      svc.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
      svc.AddTransient<IDbConnection>((_) => new SqlConnection(config.GetConnectionString("Library")));
      return svc;
    }
  }
}