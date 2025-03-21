using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class AddPlayerToTeamRequest
{
  [Required]
  public int PlayerId { get; set; }

  [Required]
  [Range(0, 99)]
  public int JerseyNumber { get; set; }
}