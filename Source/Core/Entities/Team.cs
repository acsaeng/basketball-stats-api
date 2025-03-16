using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketballStatsApi.Core.Entities;

public class Team
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int TeamId { get; set; }

  [MaxLength(10)]
  public string Status { get; set; }

  [MaxLength(25)]
  public string Locale { get; set; }

  [MaxLength(25)]
  public string Name { get; set; }

  [MaxLength(3)]
  public string Abbreviation { get; set; }

  [MaxLength(25)]
  public string Location { get; set; }

  [MaxLength(25)]
  public string Stadium { get; set; }

  public ICollection<Player> Players { get; set; }

  public int Wins { get; set; }

  public int Losses { get; set; }
}