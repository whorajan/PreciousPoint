using Microsoft.AspNetCore.Mvc;
using PreciousPoint.Models.DataModel.Account;
using PreciousPoint.Models.ViewModel.Account;

namespace PreciousPoint.Application.Interfaces.Repository
{
  public interface IUserRepository
  {

    Task<User?> GetUserByIdAsync(int id);

    Task<User?> GetUserByUserNameAsync(string userName);

    Task<bool> EmailAlreadyExists(string email);

    Task<Role?> GetRegistrationDefaultRole();
  }
}

