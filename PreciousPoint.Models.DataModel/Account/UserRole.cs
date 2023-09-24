using Microsoft.AspNetCore.Identity;

namespace PreciousPoint.Models.DataModel.Account
{
#pragma warning disable CS8618
  public class UserRole : IdentityUserRole<int>
  {
    public int Id { get; set; }

    public User User { get; set; }

    public Role Role { get; set; }
  }
#pragma warning restore CS8618
}