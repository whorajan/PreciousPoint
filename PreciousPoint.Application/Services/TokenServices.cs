using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PreciousPoint.Application.Interfaces.Services;
using PreciousPoint.Models.DataModel.Account;

namespace PreciousPoint.Application.Services
{
  public class TokenServices : ITokenService
  {
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly SymmetricSecurityKey _key;

    public TokenServices(IConfiguration configuration, UserManager<User> userManager)
    {
      this._configuration = configuration;
      _userManager = userManager;
      _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:tokenKey"] ?? ""));
    }

    public async Task<string> CreateToken(User user)
    {
      var claim = new List<Claim>()
      {
        new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? ""),
      };
      var roles = await _userManager.GetRolesAsync(user);
      var issuer = _configuration["jwt:issuer"];
      var audience = _configuration["jwt:audience"];
      claim.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
      var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claim),
        Expires = DateTime.Now.AddDays(1),
        SigningCredentials = cred,
        Issuer = issuer,
        Audience = audience,
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}