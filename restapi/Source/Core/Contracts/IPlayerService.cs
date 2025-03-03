using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Contracts;

public interface IPlayerService
{
  Task<PlayerResponse?> GetPlayer(int playerId);

  Task<PlayerResponse> AddPlayer(AddPlayerRequest addPlayerRequest);
  
  Task UpdatePlayerInfo(int playerId, UpdatePlayerInfoRequest updatePlayerInfoRequest);
  
  Task UpdatePlayerInjury(int id, PlayerInjuryRequest playerInjuryRequest);

  Task UpdatePlayerTeam(int playerId, PlayerTeamRequest playerTeamRequest);
}