using BasketballLeagueApi.Core.Dtos.Requests;
using BasketballLeagueApi.Core.Dtos.Responses;
using BasketballLeagueApi.Core.Entities;

namespace BasketballLeagueApi.Core.Contracts;

public interface IPlayerService
{
  Task<PlayerResponse?> GetPlayer(int playerId);

  Task<ICollection<PlayerResponse>> GetLeagueLeaders(string statType);

  Task<PlayerResponse> CreatePlayer(CreatePlayerRequest request);

  Task<PlayerResponse?> UpdatePlayerInfo(int playerId, UpdatePlayerInfoRequest request);

  Task<PlayerResponse?> UpdatePlayerInjuryStatus(int playerId, UpdatePlayerInjuryRequest request);

  Task<PlayerResponse?> UpdatePlayerRosterStatus(int playerId, UpdatePlayerRosterStatusRequest request);
}