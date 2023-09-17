using System.ComponentModel.DataAnnotations;
using PreciousPoint.Models.DataModel.Master;

namespace PreciousPoint.Models.DataModel.Account
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public class User
  {
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string? PhoneNo { get; set; }

    public Address? Address { get; set; }
  }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable
}