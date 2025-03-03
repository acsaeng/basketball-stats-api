using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Contracts;

public interface IPlayerService
{
  Task<Player?> GetPlayer(int playerId);

  Task<Player> AddPlayer(Player player);
  
  Task UpdatePlayerInfo(int id, Player player);
  
  Task UpdatePlayerInjury(int id, PlayerInjuryRequest playerInjuryRequest);

  Task UpdatePlayerTeam(int playerId, PlayerTeamRequest playerTeamRequest);
}