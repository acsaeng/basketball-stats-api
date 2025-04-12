using System.ComponentModel.DataAnnotations;

namespace BasketballLeagueApi.Core.Dtos.Requests;

public class AddPlayerToRosterRequest
{
  [Required]
  public int PlayerId { get; set; }

  [Required]
  [Range(0, 99)]
  public int JerseyNumber { get; set; }
}