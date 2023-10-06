using PreciousPoint.Models.DataModel.Account;

namespace PreciousPoint.Application.Interfaces.Repository
{
  public interface IUserRepository
  {
    void Update(User user);

    Task<User?> GetUserByIdAsync(int id);

    Task<User?> GetUserByUserNameAsync(string userName);
  }
}

