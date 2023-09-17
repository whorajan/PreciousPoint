using System.ComponentModel.DataAnnotations;

namespace PreciousPoint.Models.DataModel.BaseModel
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public class BaseMasterModel
  {
    public int Id { get; set; }

    [Required]
    [MaxLength(30, ErrorMessage = "Name should be 30 characters max")]
    public string Name { get; set; }
  }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}