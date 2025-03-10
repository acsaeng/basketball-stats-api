using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class UpdatePlayerTeamRequest
{
  [Required]
  public string? RosterStatus { get; set; }
  
  [Required]
  public string? Team { get; set; }

  [Required]
  public int? JerseyNumber { get; set; }
}