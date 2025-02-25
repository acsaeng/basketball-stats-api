using BasketballStatsApi.Core.Contracts;
using BasketballStatsApi.Core.Entities;
using BasketballStatsApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BasketballStatsApi.Services;

public class PlayerService : IPlayerService
{
  private readonly DataContext _context;

  public PlayerService(DataContext context)
  {
    _context = context;
  }

  public async Task<Player?> GetPlayer(int playerId)
  {
    var player = await _context.Players.FirstOrDefaultAsync(player => player.PlayerId == playerId);
    return player;
  }

  public async Task<Player> AddPlayer(Player player)
  {
    _context.Players.Add(player);
    await _context.SaveChangesAsync();
    return player;
  }
}