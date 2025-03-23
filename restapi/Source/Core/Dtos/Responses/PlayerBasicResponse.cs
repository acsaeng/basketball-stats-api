namespace BasketballStatsApi.Core.Dtos.Responses;

public class PlayerBasicResponse
{
  public int PlayerId { get; set; }

  public string FirstName { get; set; }

  public string LastName { get; set; }

  public DateOnly Dob { get; set; }

  public int Height { get; set; }

  public int Weight { get; set; }

  public string Position { get; set; }

  public string? InjuryStatus { get; set; }

  public int? JerseyNumber { get; set; }
}