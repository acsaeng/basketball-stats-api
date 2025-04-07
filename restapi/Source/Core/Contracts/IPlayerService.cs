using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Contracts;

public interface IPlayerService
{
  Task<PlayerResponse?> GetPlayer(int playerId);

  Task<ICollection<PlayerResponse>> GetLeagueLeaders(string statType);

  Task<PlayerResponse> CreatePlayer(CreatePlayerRequest request);

  Task<PlayerResponse?> UpdatePlayerInfo(int playerId, UpdatePlayerInfoRequest request);

  Task<PlayerResponse?> UpdatePlayerInjuryStatus(int playerId, UpdatePlayerInjuryRequest request);

  Task<PlayerResponse?> UpdatePlayerRosterStatus(int playerId, UpdatePlayerRosterStatusRequest request);
}