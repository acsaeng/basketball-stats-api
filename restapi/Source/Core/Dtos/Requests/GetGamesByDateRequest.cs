using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApi.Core.Dtos.Requests;

public class GetGamesByDateRequest
{
  [Required]
  public DateOnly Date { get; set; }
}