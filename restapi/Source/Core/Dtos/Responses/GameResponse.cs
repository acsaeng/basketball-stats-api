using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Dtos.Responses;

public class GameResponse
{
  public int GameId { get; set; }

  public String Status { get; set; }

  public DateTime DateTime { get; set; }

  public string HomeTeam { get; set; }

  public string AwayTeam { get; set; }
  
  public int? PointsHome { get; set; }

  public int? PointsAway { get; set; }

  public ICollection<Player> Players { get; } = new List<Player>();
}