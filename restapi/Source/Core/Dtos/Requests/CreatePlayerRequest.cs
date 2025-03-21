using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class CreatePlayerRequest
{
  [Required]
  [MaxLength(50)]
  public string FirstName { get; set; }

  [Required]
  [MaxLength(50)]
  public string LastName { get; set; }

  [Required]
  public DateOnly Dob { get; set; }

  [Required]
  public int Height { get; set; }

  [Required]
  public int Weight { get; set; }

  [Required]
  [AllowedValues("PG", "SG", "SF", "PF", "C", ErrorMessage = "Field must be one of ['PG', 'SG', 'SF', 'PF', 'C']")]
  public string Position { get; set; }
}