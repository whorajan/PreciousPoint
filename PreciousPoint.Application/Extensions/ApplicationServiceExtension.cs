using System;
using Microsoft.EntityFrameworkCore;
using PreciousPoint.Application.DataLayer;

namespace PreciousPoint.Application.Extensions
{
  public static class ApplicationServiceExtension
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<BaseDataContext>(options =>
       options.UseSqlite(configuration.GetConnectionString("PreciousPointConnection")));

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      return services;
    }
  }
}

