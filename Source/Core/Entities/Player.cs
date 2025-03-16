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

  public int Height { get; set; }

  public int Weight { get; set; }

  [MaxLength(10)]
  public string Position { get; set; }

  [MaxLength(10)]
  public string? InjuryStatus { get; set; }

  [MaxLength(10)]
  public string? RosterStatus { get; set; }

  public Team? Team { get; set; }

  public int? JerseyNumber { get; set; }

  [Precision(7, 5)]
  public decimal Points { get; set; }

  [Precision(7, 5)]
  public decimal Assists { get; set; }

  [Precision(7, 5)]
  public decimal Rebounds { get; set; }

  [Precision(7, 5)]
  public decimal Steals { get; set; }

  [Precision(7, 5)]
  public decimal Blocks { get; set; }

  [Precision(7, 5)]
  public decimal Turnovers { get; set; }
}