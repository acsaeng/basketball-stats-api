namespace BasketballStatsApi.Core.Dtos.Helpers;

public class TeamPlayer
{
  public int PlayerId { get; set; }

  public string FirstName { get; set; }

  public string LastName { get; set; }

  public string Position { get; set; }

  public string InjuryStatus { get; set; }

  public int JerseyNumber { get; set; }
}