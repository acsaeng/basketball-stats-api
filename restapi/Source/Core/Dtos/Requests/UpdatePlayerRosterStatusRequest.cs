using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class UpdatePlayerRosterStatusRequest
{
  [Required]
  [AllowedValues("Free agent", "Retired", ErrorMessage = "Field must be one of ['Free agent', 'Retired']")]
  public string? RosterStatus { get; set; }
}