using System.ComponentModel.DataAnnotations;

namespace PreciousPoint.Models.ViewModel.Account
{
#pragma warning disable CS8618
  public class RegisterModel
  {

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

    [MaxLength(15,ErrorMessage = "Phone No. should be 15 characters max")]
    public string? PhoneNo { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(15,ErrorMessage ="Password must be between 7 and 15 characters",MinimumLength =7)]
    public string Password { get; set; }
  }
#pragma warning restore CS8618
}