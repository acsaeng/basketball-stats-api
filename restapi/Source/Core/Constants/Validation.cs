namespace BasketballStatsApi.Core.Constants;

public static class Validation
{
  public static class Player
  {
    public static class InjuryStatus
    {
      public const string Healthy = "Healthy";
      public const string DayToDay = "Day-to-day";
      public const string Out = "Out";
    }

    public static class RosterStatus
    {
      public const string Active = "Active";
      public const string FreeAgent = "Free agent";
      public const string Retired = "Retired";
    }

    public static class Position
    { 
      public const string PointGuard = "PG";
      public const string ShootingGuard = "SG";
      public const string SmallForward = "SF";
      public const string PowerForward = "PF";
      public const string Centre = "C";
    }

    public static class StatTypes
    {
      public const string Points = "points";
      public const string Assists = "assists";
      public const string Rebounds = "rebounds";
      public const string Steals = "steals";
      public const string Blocks = "blocks";
      public const string Turnovers = "turnovers";
    }
  }

  public static class Team
  {
    public static class Status
    {
      public const string Active = "Active";
      public const string Defunct = "Defunct";
    }

    public const int MaxTeamRoster = 12;
  }

  public static class Game
  {
    public static class Status
    {
      public const string Upcoming = "Upcoming";
      public const string InProgress = "In progress";
      public const string Final = "Final";
      public const string Postponed = "Postponed";
      public const string Cancelled = "Cancelled";
    }
  }
}