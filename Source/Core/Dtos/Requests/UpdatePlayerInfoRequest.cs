using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class UpdatePlayerInfoRequest
{
  [Required]
  public string FirstName { get; set; }

  [Required]
  public string LastName { get; set; }

  [Required]
  public DateOnly Dob { get; set; }

  [Required]
  public int Height { get; set; }

  [Required]
  public int Weight { get; set; }

  [Required]
  public string Position { get; set; }
}