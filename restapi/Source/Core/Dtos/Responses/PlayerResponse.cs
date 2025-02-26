namespace BasketballStatsApi.Core.Dtos.Responses;

public class PlayerResponse
{
  public int PlayerId { get; set; }

  public string FirstName { get; set; }

  public string LastName { get; set; }

  public DateOnly Dob { get; set; }

  public int Height { get; set; }

  public int Weight { get; set; }

  public string Position { get; set; }
  
  public string? InjuryStatus { get; set; }
  
  public string? RosterStatus { get; set; }

  // TODO: associate with Team entity once created
  public string? Team { get; set; }

  public int? JerseyNumber { get; set; }

  public decimal Points { get; set; }

  public decimal Assists { get; set; }

  public decimal Rebounds { get; set; }

  public decimal Steals { get; set; }

  public decimal Blocks { get; set; }

  public decimal Turnovers { get; set; }
}