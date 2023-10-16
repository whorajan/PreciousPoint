using System.ComponentModel.DataAnnotations;

namespace PreciousPoint.Models.ViewModel.Account
{
#pragma warning disable CS8618
  public class LoginModel
  {
    [Required]
    [EmailAddress]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(15, ErrorMessage = "Password must be between 7 and 15 characters", MinimumLength = 7)]
    public string Password { get; set; }
  }
#pragma warning restore CS8618
}

