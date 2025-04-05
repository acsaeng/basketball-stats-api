using System.ComponentModel.DataAnnotations;
using BasketballStatsApi.Core.Constants;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class UpdateGameStatusRequest
{
  [Required]
  [AllowedValues(
    Validation.Game.Status.Upcoming,
    Validation.Game.Status.InProgress,
    Validation.Game.Status.Final,
    Validation.Game.Status.Postponed,
    Validation.Game.Status.Cancelled,
    ErrorMessage = Error.Game.InvalidStatus
  )]
  public string Status { get; set; }
}