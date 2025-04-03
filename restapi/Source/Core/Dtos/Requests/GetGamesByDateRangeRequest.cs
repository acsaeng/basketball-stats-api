using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class GetGamesByDateRangeRequest
{
  [Required]
  public DateOnly DateStart { get; set; }

  [Required]
  public DateOnly DateEnd { get; set; }
}