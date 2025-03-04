namespace BasketballStatsApi.Core.Dtos.Requests;

public class UpdatePlayerTeamRequest
{
  public string? RosterStatus { get; set; }
  
  public string? Team { get; set; }

  public int? JerseyNumber { get; set; }
}