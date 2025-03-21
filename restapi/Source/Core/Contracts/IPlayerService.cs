using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Contracts;

public interface IPlayerService
{
  Task<PlayerResponse?> GetPlayer(int playerId);

  Task<PlayerResponse> CreatePlayer(CreatePlayerRequest createPlayerRequest);

  Task<PlayerResponse?> UpdatePlayerInfo(int playerId, UpdatePlayerInfoRequest updatePlayerInfoRequest);

  Task<PlayerResponse?> UpdatePlayerInjury(int playerId, UpdatePlayerInjuryRequest updatePlayerInjuryRequest);

  Task<PlayerResponse?> UpdatePlayerRosterStatus(int playerId, UpdatePlayerRosterStatusRequest updatePlayerRosterStatusRequest);
}