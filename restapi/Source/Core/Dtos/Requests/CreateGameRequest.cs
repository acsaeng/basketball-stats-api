using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class CreateGameRequest
{
  public DateTime DateTime { get; set; }

  public int HomeTeamId { get; set; }

  public int AwayTeamId { get; set; }
}