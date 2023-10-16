using PreciousPoint.Models.DataModel.Account;

namespace PreciousPoint.Application.Interfaces.Services
{
  public interface ITokenService
  {
    Task<string> CreateToken(User user);

  }
}

