using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BasketballStatsApi.Core.Entities;

public class Game
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int GameId { get; set; }

  [AllowedValues("Upcoming", "In progress", "Final", "Postponed")]
  public String Status { get; set; }

  public DateTime DateTime { get; set; }

  // Foreign key
  public int HomeTeamId { get; set; }

  // Reference navigation
  public Team HomeTeam { get; set; }

  // Foreign key
  public int AwayTeamId { get; set; }

  // Reference navigation
  public Team AwayTeam { get; set; }

  [Range(0, int.MaxValue)]
  public int? PointsHome { get; set; }

  [Range(0, int.MaxValue)]
  public int? PointsAway { get; set; }

  // Collection navigation
  public ICollection<Player> PlayerStats { get; } = new List<Player>();
}