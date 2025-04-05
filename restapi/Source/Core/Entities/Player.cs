using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BasketballStatsApi.Core.Constants;
using Microsoft.EntityFrameworkCore;

namespace BasketballStatsApi.Core.Entities;

public class Player
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int PlayerId { get; set; }

  [MaxLength(50)]
  public string FirstName { get; set; }

  [MaxLength(50)]
  public string LastName { get; set; }

  public DateOnly Dob { get; set; }

  [Range(0, int.MaxValue)]
  public int Height { get; set; }

  [Range(0, int.MaxValue)]
  public int Weight { get; set; }

  [AllowedValues(
    Validation.Player.Position.PointGuard,
    Validation.Player.Position.ShootingGuard,
    Validation.Player.Position.SmallForward,
    Validation.Player.Position.PowerForward,
    Validation.Player.Position.Centre
  )]
  public string Position { get; set; }

  [AllowedValues(
    Validation.Player.InjuryStatus.Healthy,
    Validation.Player.InjuryStatus.DayToDay,
    Validation.Player.InjuryStatus.Out
  )]
  public string? InjuryStatus { get; set; }

  [AllowedValues(
    Validation.Player.RosterStatus.Active,
    Validation.Player.RosterStatus.FreeAgent,
    Validation.Player.RosterStatus.Retired
  )]
  public string RosterStatus { get; set; }

  // Foreign key
  public int? TeamId { get; set; }

  // Reference navigation
  public Team? Team { get; set; }

  [Range(0, 99)]
  public int? JerseyNumber { get; set; }

  [Range(0, int.MaxValue)]
  public int GamesPlayed { get; set; }

  [Range(0, int.MaxValue)]
  [Precision(7, 5)]
  public decimal Points { get; set; }

  [Range(0, int.MaxValue)]
  [Precision(7, 5)]
  public decimal Assists { get; set; }

  [Range(0, int.MaxValue)]
  [Precision(7, 5)]
  public decimal Rebounds { get; set; }

  [Range(0, int.MaxValue)]
  [Precision(7, 5)]
  public decimal Steals { get; set; }

  [Range(0, int.MaxValue)]
  [Precision(7, 5)]
  public decimal Blocks { get; set; }

  [Range(0, int.MaxValue)]
  [Precision(7, 5)]
  public decimal Turnovers { get; set; }

  // Collection navigation
  public ICollection<PlayerGame> GameStats { get; } = new List<PlayerGame>();
}