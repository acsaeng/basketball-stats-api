using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class UpdatePlayerTeamRequest
{
  [Required]
  [AllowedValues("Active", "Free agent", "Retired", ErrorMessage = "Field must be one of ['Active', 'Free agent', 'Retired']")]
  public string? RosterStatus { get; set; }

  public string? Team { get; set; }

  [Range(0, 99)]
  public int? JerseyNumber { get; set; }
}