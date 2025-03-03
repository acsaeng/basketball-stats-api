namespace BasketballStatsApi.Core.Dtos.Requests;

public class PlayerInfoRequest
{
  public string FirstName { get; set; }

  public string LastName { get; set; }

  public DateOnly Dob { get; set; }

  public int Height { get; set; }

  public int Weight { get; set; }

  public string Position { get; set; }
}