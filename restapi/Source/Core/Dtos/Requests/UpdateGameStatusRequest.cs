using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class UpdateGameStatusRequest
{
  [Required]
  [AllowedValues("Upcoming", "In progress", "Final", "Postponed", "Cancelled",
    ErrorMessage = "Field must be one of ['Upcoming', 'In progress', 'Final', 'Postponed', 'Cancelled']")]
  public string Status { get; set; }
}