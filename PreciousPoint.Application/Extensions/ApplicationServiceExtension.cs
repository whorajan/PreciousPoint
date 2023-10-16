using Microsoft.EntityFrameworkCore;
using PreciousPoint.Application.DataLayer;
using PreciousPoint.Application.Interfaces.Repository;
using PreciousPoint.Application.Interfaces.Services;
using PreciousPoint.Application.Services;

namespace PreciousPoint.Application.Extensions
{
  public static class ApplicationServiceExtension
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<BaseDataContext>(options =>
       options.UseSqlite(configuration.GetConnectionString("PreciousPointConnection")));

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<ITokenService, TokenServices>();
      return services;
    }
  }
}
