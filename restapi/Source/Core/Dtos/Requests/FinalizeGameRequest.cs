using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class FinalizeGameRequest
{
  [Required]
  [Range(0, int.MaxValue)]
  public int HomeTeamPoints { get; set; }

  [Required]
  [Range(0, int.MaxValue)]
  public int AwayTeamPoints { get; set; }

  [Required]
  public ICollection<FinalizeGameRequestPlayerStats> HomeTeamPlayerStats { get; set; } = new List<FinalizeGameRequestPlayerStats>();

  [Required]
  public ICollection<FinalizeGameRequestPlayerStats> AwayTeamPlayerStats { get; set; } = new List<FinalizeGameRequestPlayerStats>();
}

public class FinalizeGameRequestPlayerStats
{
  [Required]
  public int PlayerId { get; set; }

  [Required]
  [Range(0, int.MaxValue)]
  public int Points { get; set; }

  [Required]
  [Range(0, int.MaxValue)]
  public int Assists { get; set; }

  [Required]
  [Range(0, int.MaxValue)]
  public int Rebounds { get; set; }

  [Required]
  [Range(0, int.MaxValue)]
  public int Steals { get; set; }

  [Required]
  [Range(0, int.MaxValue)]
  public int Blocks { get; set; }

  [Required]
  [Range(0, int.MaxValue)]
  public int Turnovers { get; set; }
}