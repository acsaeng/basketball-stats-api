using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class FinalizeGameRequest
{
  [Required]
  [Range(0, int.MaxValue)]
  public int PointsHome { get; set; }
  
  [Required]
  [Range(0, int.MaxValue)]
  public int PointsAway { get; set; }
  
  [Required]
  public ICollection<FinalizeGamePlayerStats> HomeTeamPlayerStats { get; set; } = new List<FinalizeGamePlayerStats>();
  
  [Required]
  public ICollection<FinalizeGamePlayerStats> AwayTeamPlayerStats { get; set; } = new List<FinalizeGamePlayerStats>();
}

public class FinalizeGamePlayerStats
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