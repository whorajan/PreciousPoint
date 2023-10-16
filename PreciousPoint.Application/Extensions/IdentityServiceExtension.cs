using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PreciousPoint.Application.DataLayer;
using PreciousPoint.Models.DataModel.Account;

namespace PreciousPoint.Application.Extensions
{
  public static class IdentityServiceExtension
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

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(o =>
      {
        o.TokenValidationParameters = new TokenValidationParameters
        {
          ValidIssuer = configuration["jwt:issuer"],
          ValidAudience = configuration["jwt:audience"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:tokenKey"] ?? "")),
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = false,
          ValidateIssuerSigningKey = true
        };
      });

      return services;
    }
  }
}

