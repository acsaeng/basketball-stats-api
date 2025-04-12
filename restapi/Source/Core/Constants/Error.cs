namespace BasketballLeagueApi.Core.Constants;

public static class Error
{
  public static class Player
  {
    public const string InvalidInjuryStatus = "Field must be one of ['Healthy', 'Day-to-day', 'Out']";
    public const string InvalidRosterStatus = "Field must be one of ['Free agent', 'Retired']";
    public const string InactivePlayer = "Cannot perform this action to an inactive player";
    public const string InvalidPosition = "Field must be one of ['PG', 'SG', 'SF', 'PF', 'C']";
    public const string InvalidStatType = "Argument must be one of ['points', 'assists', 'rebounds', 'steals', 'blocks', 'turnovers']";
  }

  public static class Team
  {
    public const string InactiveTeam = "Cannot perform this action to an inactive team";
    public const string InvalidNameOrAbbr = "Team cannot have the same name or abbreviation as an existing team";
    public const string MaxRosterExceeded = "Maximum number of players on roster exceeded";
    public const string InvalidJerseyNumber = "The selected jersey number is already taken by another player on the team";
  }

  public static class Game
  {
    public const string InvalidState = "Cannot update game based on current state";
    public const string InvalidStatus = "Field must be one of ['Upcoming', 'In progress', 'Final', 'Postponed', 'Cancelled']";
    public const string InvalidDate = "Cannot update game at this current date and time";
    public const string InvalidDateRange = "Start date must occur before end date";
    public const string InvalidPlayer = "One of the players listed is not on either participating teams";
    public const string PlayerNotFound = "One of the players could not be found";
    public const string TeamNotFound = "At least one of the teams could not be found";
    public const string PointsNotEqual = "Total points between teams and players are not equal";
    public const string TiesNotAllowed = "Game cannot result in a tie";
  }
}