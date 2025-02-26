using BasketballStatsApi.Core.Contracts;
using BasketballStatsApi.Core.Entities;
using BasketballStatsApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BasketballStatsApi.Services;

public class PlayerService(DataContext context) : IPlayerService
{
  public async Task<Player?> GetPlayer(int playerId)
  {
    var player = await context.Players.FindAsync(playerId);
    return player;
  }

  public async Task<Player> AddPlayer(Player player)
  {
    context.Players.Add(player);
    await context.SaveChangesAsync();
    return player;
  }

  public async Task MakePlayerFreeAgent(int playerId)
  {
    var player = await context.Players.FindAsync(playerId);

    if (player is not null)
    {
      player.RosterStatus = "Free Agent";
      player.Team = null;
      player.JerseyNumber = null;
      await context.SaveChangesAsync();
    }
  }

  public async Task RetirePlayer(int playerId)
  {
    var player = await context.Players.FindAsync(playerId);

    if (player is not null)
    {
      player.RosterStatus = "Retired";
      player.InjuryStatus = null;
      player.Team = null;
      player.JerseyNumber = null;
      await context.SaveChangesAsync();
    }
  }
}