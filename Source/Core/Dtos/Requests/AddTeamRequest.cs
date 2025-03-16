using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class AddTeamRequest
{
  [Required]
  public string Locale { get; set; }

  [Required]
  public string Name { get; set; }

  [Required]
  public string Abbreviation { get; set; }

  [Required]
  public string Location { get; set; }

  [Required]
  public string Stadium { get; set; }
}