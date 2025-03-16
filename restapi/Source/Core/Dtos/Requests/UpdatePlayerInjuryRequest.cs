using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class UpdatePlayerInjuryRequest
{
  [Required]
  [AllowedValues("Healthy", "Day-to-day", "Out", ErrorMessage = "Field must be one of ['Healthy', 'Day-to-day', 'Out']")]
  public string InjuryStatus { get; set; }
}