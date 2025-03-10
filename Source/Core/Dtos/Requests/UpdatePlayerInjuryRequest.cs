using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class UpdatePlayerInjuryRequest
{
  [Required]
  public string InjuryStatus { get; set; }
}