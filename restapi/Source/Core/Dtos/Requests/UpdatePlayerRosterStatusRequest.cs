using System.ComponentModel.DataAnnotations;
using BasketballStatsApi.Core.Constants;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class UpdatePlayerRosterStatusRequest
{
  [Required]
  [AllowedValues(
    Validation.Player.RosterStatus.FreeAgent,
    Validation.Player.RosterStatus.Retired,
    ErrorMessage = Error.Player.InvalidRosterStatus)]
  public string RosterStatus { get; set; }
}