using System.ComponentModel.DataAnnotations;

namespace PreciousPoint.Models.DataModel.Master
{
#pragma warning disable CS8618
  public class Address
  {
    public int Id { get; set; }

    [Required]
    [MaxLength(10, ErrorMessage = $"{nameof(Type)} should be 10 characters max")]
    public string Type { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = $"{nameof(AddressLine1)} should be 100 characters max")]
    public string AddressLine1 { get; set; }

    [MaxLength(100, ErrorMessage = $"{nameof(AddressLine2)} should be 100 characters max")]
    public string? AddressLine2 { get; set; }

    [MaxLength(100, ErrorMessage = $"{nameof(AddressLine3)} should be 100 characters max")]
    public string? AddressLine3 { get; set; }

    [MaxLength(50, ErrorMessage = $"{nameof(ContactPerson)} should be 30 characters max")]
    public string? ContactPerson { get; set; }

    [MaxLength(20, ErrorMessage = $"{nameof(ContactNumber)} should be 20 characters max")]
    public string? ContactNumber { get; set; }

    [MaxLength(10, ErrorMessage = $"{nameof(PinCode)} should be 10 characters max")]
    public string? PinCode { get; set; }

    public City? City { get; set; }
  }
#pragma warning restore CS8618
}