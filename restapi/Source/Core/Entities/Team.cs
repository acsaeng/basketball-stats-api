using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BasketballStatsApi.Core.Entities;

public class Team
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int TeamId { get; set; }

  [AllowedValues("Active", "Defunct")]
  public string Status { get; set; }

  [MaxLength(50)]
  public string Locale { get; set; }

  [MaxLength(50)]
  public string Name { get; set; }

  [MaxLength(3)]
  public string Abbreviation { get; set; }

  [MaxLength(50)]
  public string Location { get; set; }

  [MaxLength(50)]
  public string Stadium { get; set; }

  [MaxLength(100)]
  public string? HeadCoach { get; set; }

  // Collection navigation
  public ICollection<Player> Roster { get; } = new List<Player>();

  [Range(0, int.MaxValue)]
  public int Wins { get; set; }

  [Range(0, int.MaxValue)]
  public int Losses { get; set; }

  [Range(0, int.MaxValue)]
  [Precision(4, 3)]
  public decimal WinPercentage { get; set; }

  // Collection navigation
  public ICollection<Game> Games { get; } = new List<Game>();
}