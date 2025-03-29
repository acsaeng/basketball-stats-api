using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

  [AllowedValues("PG", "SG", "SF", "PF", "C")]
  public string Position { get; set; }

  [AllowedValues("Healthy", "Day-to-day", "Out")]
  public string? InjuryStatus { get; set; }

  [AllowedValues("Active", "Free agent", "Retired")]
  public string RosterStatus { get; set; }

  // Foreign key
  public int? TeamId { get; set; }

  // Reference navigation
  public Team? Team { get; set; }

  [Range(0, 99)]
  public int? JerseyNumber { get; set; }

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
  public ICollection<PlayerGameStats> GameStats { get; } = new List<PlayerGameStats>();
}