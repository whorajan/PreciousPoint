using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PreciousPoint.Models.DataModel.Account
{
#pragma warning disable CS8618
  [Table(nameof(Role))]
  public class Role : IdentityRole<int>
  {
    public ICollection<UserRole> UserRoles { get; set; }
  }
#pragma warning restore CS8618
}