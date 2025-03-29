using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BasketballStatsApi.Core.Entities;

[PrimaryKey("PlayerId", "GameId")]
public class PlayerGameStats
{
  [Key]
  public Player Player { get; set; }
  
  [Key]
  public Game Game { get; set; }
  
  [Range(0, int.MaxValue)]
  public int Points { get; set; }
  
  [Range(0, int.MaxValue)]
  public int Assists { get; set; }
  
  [Range(0, int.MaxValue)]
  public int Rebounds { get; set; }
  
  [Range(0, int.MaxValue)]
  public int Steals { get; set; }
  
  [Range(0, int.MaxValue)]
  public int Blocks { get; set; }
  
  [Range(0, int.MaxValue)]
  public int Turnovers { get; set; }
}