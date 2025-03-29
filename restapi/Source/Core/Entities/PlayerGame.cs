using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BasketballStatsApi.Core.Entities;

[PrimaryKey("PlayerId", "GameId")]
public class PlayerGame
{
  // Foreign key
  [Key]
  public int PlayerId { get; set; }

  // Reference navigation
  public Player Player { get; set; }

  // Foreign key
  [Key]
  public int GameId { get; set; }

  // Reference navigation
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