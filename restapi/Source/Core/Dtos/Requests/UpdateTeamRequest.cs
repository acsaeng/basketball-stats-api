using System.ComponentModel.DataAnnotations;

namespace BasketballLeagueApi.Core.Dtos.Requests;

public class UpdateTeamRequest
{
  [Required]
  [MaxLength(50)]
  public string Locale { get; set; }

  [Required]
  [MaxLength(50)]
  public string Name { get; set; }

  [Required]
  [MaxLength(3)]
  public string Abbreviation { get; set; }

  [Required]
  [MaxLength(50)]
  public string Location { get; set; }

  [Required]
  [MaxLength(50)]
  public string Stadium { get; set; }

  [Required]
  [MaxLength(100)]
  public string HeadCoach { get; set; }
}