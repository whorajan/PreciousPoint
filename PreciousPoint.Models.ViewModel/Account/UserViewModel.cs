using System.ComponentModel.DataAnnotations;
using PreciousPoint.Models.DataModel.Account;
using PreciousPoint.Models.ViewModel.Master;

namespace PreciousPoint.Models.ViewModel.Account
{
#pragma warning disable CS8618
  public class UserViewModel
  {
    public int Id { get; set; }

    [Required]
    [MaxLength(30,ErrorMessage ="First Name should be 30 characters max")]
    public string FirstName { get; set; }

    [MaxLength(30,ErrorMessage = "Middle Name should be 30 characters max")]
    public string? MiddleName { get; set; }

    [MaxLength(30,ErrorMessage = "Last Name should be 30 characters max")]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string? PhoneNo { get; set; }

    public string Token { get; set; }

    public AddressViewModel? Address { get; set; }

    public ICollection<RoleViewModel>? Roles { get; set; }

  }
#pragma warning restore CS8618
}