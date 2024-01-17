using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameLib.Infrastructure.Data;

public static class AppDbContextExtensions
{
 public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
  {
    string? connectionString = configuration.GetConnectionString("SqlServerConnection");
    if(string.IsNullOrEmpty(connectionString)) throw new Exception("Connection String Not Found");

    services.AddDbContext<AppDbContext>(options =>
         options.UseSqlServer(connectionString));
  }
}
