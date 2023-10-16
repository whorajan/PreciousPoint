using Microsoft.AspNetCore.Identity;
using PreciousPoint.Application.Interfaces.Repository;
using PreciousPoint.Helpers.EmailCommunication;
using PreciousPoint.Models.DataModel.Account;

namespace PreciousPoint.Application.Services
{
  public class UserRepository : IUserRepository
  {
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public UserRepository(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
      _userManager = userManager;
      _roleManager = roleManager;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
      return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<User?> GetUserByUserNameAsync(string userName)
    {
      return await _userManager.FindByNameAsync(userName);
    }

    public async Task<bool>  EmailAlreadyExists(string emailId)
    {
      return await _userManager.FindByEmailAsync(emailId) != null;
    }

    public async Task<Role?> GetRegistrationDefaultRole()
    {
      return await _roleManager.FindByNameAsync("user");
    }
  }
}

