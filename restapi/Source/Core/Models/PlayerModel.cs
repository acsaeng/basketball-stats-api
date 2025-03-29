using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Models;

public class PlayerModel
{
  public int GamesPlayed { get; set; }
  
  public decimal Points { get; set; }
  
  public decimal Assists { get; set; }
  
  public decimal Rebounds { get; set; }
  
  public decimal Steals { get; set; }
  
  public decimal Blocks { get; set; }

  public decimal Turnovers { get; set; }

  public void AddGamesStats(PlayerGameStats gameStats)
  {
    GamesPlayed++;
    Points += (gameStats.Points - Points) / GamesPlayed;
    Assists += (gameStats.Assists - Assists) / GamesPlayed;
    Rebounds += (gameStats.Rebounds - Rebounds) / GamesPlayed;
    Steals += (gameStats.Steals - Steals) / GamesPlayed;
    Blocks += (gameStats.Blocks - Blocks) / GamesPlayed;
    Turnovers += (gameStats.Turnovers - Turnovers) / GamesPlayed;
  }
}