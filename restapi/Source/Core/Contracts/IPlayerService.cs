using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Contracts;

public interface IPlayerService
{
  Task<Player?> GetPlayer(int playerId);

  Task<Player> AddPlayer(Player player);
}