using BasketballStatsApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasketballStatsApi.Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
  public DbSet<Player> Players { get; set; }
}