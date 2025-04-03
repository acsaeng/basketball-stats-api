using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;

namespace BasketballStatsApi.Core.Contracts;

public interface IGameService
{
  Task<GameResponse?> GetGameById(int gameId);

  Task<ICollection<GameResponse>> GetGamesByDateRange(GetGamesByDateRangeRequest request);

  Task<GameResponse?> CreateGame(CreateGameRequest request);

  Task<GameResponse?> UpdateGameInfo(int gameId, UpdateGameInfoRequest request);

  Task<GameResponse?> UpdateGameStatus(int gameId, UpdateGameStatusRequest request);

  Task<GameResponse?> FinalizeGame(int gameId, FinalizeGameRequest request);
}