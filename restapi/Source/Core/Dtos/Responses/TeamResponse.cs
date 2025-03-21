using BasketballStatsApi.Core.Dtos.Helpers;
using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Dtos.Responses;

public class TeamResponse
{
  public int TeamId { get; set; }

  public string Status { get; set; }

  public string Locale { get; set; }

  public string Name { get; set; }

  public string Abbreviation { get; set; }

  public string Location { get; set; }

  public string Stadium { get; set; }

  public int Wins { get; set; }

  public int Losses { get; set; }

  public ICollection<TeamPlayer> Players { get; set; }
}