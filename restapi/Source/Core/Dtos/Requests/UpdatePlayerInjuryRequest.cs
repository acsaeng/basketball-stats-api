using System.ComponentModel.DataAnnotations;
using BasketballStatsApi.Core.Constants;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class UpdatePlayerInjuryRequest
{
  [Required]
  [AllowedValues(
    Validation.Player.InjuryStatus.Healthy,
    Validation.Player.InjuryStatus.DayToDay,
    Validation.Player.InjuryStatus.Out,
    ErrorMessage = Error.Player.InvalidInjuryStatus
  )]
  public string InjuryStatus { get; set; }
}