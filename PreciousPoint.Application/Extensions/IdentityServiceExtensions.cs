using Microsoft.AspNetCore.Identity;
using PreciousPoint.Application.DataLayer;
using PreciousPoint.Models.DataModel.Account;

namespace PreciousPoint.Application.Extensions
{
  public static class IdentityServiceExtensions
  {
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddIdentityCore<User>(options =>
      {
        options.Password.RequireNonAlphanumeric = false;
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = true;
      })
        .AddRoles<Role>()
        .AddRoleManager<RoleManager<Role>>()
        .AddEntityFrameworkStores<BaseDataContext>()
        .AddDefaultTokenProviders();

      return services;
    }
  }
}

