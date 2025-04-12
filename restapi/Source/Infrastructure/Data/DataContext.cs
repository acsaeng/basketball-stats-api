using BasketballLeagueApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasketballLeagueApi.Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
  public DbSet<Player> Players { get; set; }

  public DbSet<Team> Teams { get; set; }

  public DbSet<Game> Games { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Game>()
      .HasOne(g => g.HomeTeam)
      .WithMany(t => t.HomeGames)
      .HasForeignKey(t => t.HomeTeamId);

    modelBuilder.Entity<Game>()
      .HasOne(g => g.AwayTeam)
      .WithMany(t => t.AwayGames)
      .HasForeignKey(t => t.AwayTeamId)
      .OnDelete(DeleteBehavior.NoAction);
  }
}