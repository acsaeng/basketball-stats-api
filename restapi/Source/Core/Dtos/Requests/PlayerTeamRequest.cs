namespace BasketballStatsApi.Core.Dtos.Requests;

public class PlayerTeamRequest
{
  public string? RosterStatus { get; set; }
  
  public string? Team { get; set; }

  public int? JerseyNumber { get; set; }
}