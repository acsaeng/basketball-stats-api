using System.ComponentModel.DataAnnotations;
using BasketballLeagueApi.Core.Constants;

namespace BasketballLeagueApi.Core.Dtos.Requests;

public class UpdatePlayerInfoRequest
{
  [Required]
  [MaxLength(50)]
  public string FirstName { get; set; }

  [Required]
  [MaxLength(50)]
  public string LastName { get; set; }

  [Required]
  public DateOnly Dob { get; set; }

  [Required]
  [Range(0, int.MaxValue)]
  public int Height { get; set; }

  [Required]
  [Range(0, int.MaxValue)]
  public int Weight { get; set; }

  [Required]
  [AllowedValues(
    Validation.Player.Position.PointGuard,
    Validation.Player.Position.ShootingGuard,
    Validation.Player.Position.SmallForward,
    Validation.Player.Position.PowerForward,
    Validation.Player.Position.Centre,
    ErrorMessage = Error.Player.InvalidPosition
  )]
  public string Position { get; set; }
}