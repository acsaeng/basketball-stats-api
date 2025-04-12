using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BasketballLeagueApi.Core.Constants;

namespace BasketballLeagueApi.Core.Entities;

public class Game
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int GameId { get; set; }

  [AllowedValues(
    Validation.Game.Status.Upcoming,
    Validation.Game.Status.InProgress,
    Validation.Game.Status.Final,
    Validation.Game.Status.Postponed,
    Validation.Game.Status.Cancelled
  )]
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

  public bool? DidHomeTeamWin { get; set; }

  [Range(0, int.MaxValue)]
  public int? HomeTeamPoints { get; set; }

  [Range(0, int.MaxValue)]
  public int? AwayTeamPoints { get; set; }

  // Collection navigation
  public ICollection<PlayerGame> PlayerStats { get; } = new List<PlayerGame>();
}