namespace BasketballStatsApi.Core.Dtos.Responses;

public class TeamResponse
{
  public int TeamId { get; set; }

  public string Status { get; set; }

  public string Locale { get; set; }

  public string Name { get; set; }

  public string Abbreviation { get; set; }

  public string Location { get; set; }

  public string Stadium { get; set; }

  public string? HeadCoach { get; set; }

  public ICollection<TeamResponsePlayerInfo> Roster { get; set; }

  public int GamesPlayed { get; set; }

  public int Wins { get; set; }

  public int Losses { get; set; }

  public decimal WinPercentage { get; set; }
}

public class TeamResponsePlayerInfo
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