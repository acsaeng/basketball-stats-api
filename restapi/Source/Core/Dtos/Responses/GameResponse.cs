using BasketballLeagueApi.Core.Entities;

namespace BasketballLeagueApi.Core.Dtos.Responses;

public class GameResponse
{
  public int GameId { get; set; }

  public String Status { get; set; }

  public DateTime DateTime { get; set; }

  public string HomeTeam { get; set; }

  public string AwayTeam { get; set; }

  public bool? DidHomeTeamWin { get; set; }

  public int? HomeTeamPoints { get; set; }

  public int? AwayTeamPoints { get; set; }

  public ICollection<GameResponsePlayerStats> HomeTeamPlayerStats { get; } = new List<GameResponsePlayerStats>();

  public ICollection<GameResponsePlayerStats> AwayTeamPlayerStats { get; } = new List<GameResponsePlayerStats>();
}

public class GameResponsePlayerStats
{
  public int PlayerId { get; set; }

  public string FirstName { get; set; }

  public string LastName { get; set; }

  public string Position { get; set; }

  public int JerseyNumber { get; set; }

  public int Points { get; set; }

  public int Assists { get; set; }

  public int Rebounds { get; set; }

  public int Steals { get; set; }

  public int Blocks { get; set; }

  public int Turnovers { get; set; }
}