﻿using System.ComponentModel.DataAnnotations;

namespace PreciousPoint.Models.ViewModel.Master
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public class AddressViewModel
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

    public CityViewModel? City { get; set; }
  }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}