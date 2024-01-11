using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameLib.Infrastructure.Data;

public static class AppDbContextExtensions
{
 public static void AddAppDbContext(this IServiceCollection services, string connectionString)
  {
    services.AddDbContext<AppDbContext>(options =>
         options.UseSqlServer(connectionString));
  }
}
