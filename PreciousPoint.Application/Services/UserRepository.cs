using Microsoft.AspNetCore.Identity;
using PreciousPoint.Application.Interfaces.Repository;
using PreciousPoint.Models.DataModel.Account;

namespace PreciousPoint.Application.Services
{
  public class UserRepository : IUserRepository
  {
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
      _userManager = userManager;
    }

    public Task<User?> GetUserByIdAsync(int id)
    {
      return _userManager.FindByIdAsync(id.ToString());
    }

    public Task<User?> GetUserByUserNameAsync(string userName)
    {
      return _userManager.FindByNameAsync(userName);
    }

    public void Update(User user)
    {

    }
  }
}

