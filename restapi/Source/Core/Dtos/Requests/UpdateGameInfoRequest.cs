using System.ComponentModel.DataAnnotations;

namespace BasketballLeagueApi.Core.Dtos.Requests;

public class UpdateGameInfoRequest
{
  [Required]
  public DateTime DateTime { get; set; }

  [Required]
  public int HomeTeamId { get; set; }

  [Required]
  public int AwayTeamId { get; set; }
}