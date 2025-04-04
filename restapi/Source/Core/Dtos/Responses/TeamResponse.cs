using System.Collections;
using BasketballStatsApi.Core.Entities;

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

  public ICollection<TeamResponseRoster> Roster { get; set; }

  public int GamesPlayed { get; set; }

  public int Wins { get; set; }

  public int Losses { get; set; }

  public decimal WinPercentage { get; set; }

  public TeamResponsePreviousGame PreviousGame { get; set; }

  public TeamResponseNextGame NextGame { get; set; }
}

public class TeamResponseRoster
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

public class TeamResponsePreviousGame
{
  public int GameId { get; set; }

  public string Status { get; set; }

  public DateTime DateTime { get; set; }

  public string HomeTeam { get; set; }

  public string AwayTeam { get; set; }

  public bool DidHomeTeamWin { get; set; }

  public int HomeTeamPoints { get; set; }

  public int AwayTeamPoints { get; set; }
}

public class TeamResponseNextGame
{
  public int GameId { get; set; }

  public string Status { get; set; }

  public DateTime DateTime { get; set; }

  public string HomeTeam { get; set; }

  public string AwayTeam { get; set; }
}