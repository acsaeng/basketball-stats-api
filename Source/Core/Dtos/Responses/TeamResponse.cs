using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Dtos.Responses;

public class TeamResponse
{
  public string LocaleName { get; set; }

  public string Name { get; set; }

  public string Abbreviation { get; set; }

  public string Location { get; set; }

  public string Stadium { get; set; }

  public ICollection<Player> Players { get; set; }

  public int Wins { get; set; }

  public int Losses { get; set; }
}