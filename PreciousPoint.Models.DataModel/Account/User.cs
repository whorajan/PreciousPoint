using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using PreciousPoint.Models.DataModel.Master;

namespace PreciousPoint.Models.DataModel.Account
{
#pragma warning disable CS8618
  [Table(nameof(User))]
  public class User : IdentityUser<int>
  {

    [Required]
    public string FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public Address? Address { get; set; }

    public virtual ICollection<UserRole>? Roles { get; set; }
  }
#pragma warning restore CS8618
}