using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;

namespace BasketballStatsApi.Core.Contracts;

public interface IGameService
{
  Task<GameResponse?> GetGame(int gameId);

  Task<GameResponse?> CreateGame(CreateGameRequest createGameRequest);

  Task<GameResponse?> UpdateGameInfo(int gameId, UpdateGameInfoRequest updateGameInfoRequest);
  
  Task<GameResponse?> UpdateGameStatus(int gameId, UpdateGameStatusRequest updateGameStatusRequest);
}