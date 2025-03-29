using BasketballStatsApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasketballStatsApi.Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
  public DbSet<Player> Players { get; set; }

  public DbSet<Team> Teams { get; set; }

  public DbSet<Game> Games { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Team>()
      .HasMany(t => t.Games)
      .WithOne(g => g.AwayTeam)
      .OnDelete(DeleteBehavior.NoAction)
      .IsRequired();
  }
}