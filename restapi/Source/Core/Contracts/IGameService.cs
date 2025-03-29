using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;

namespace BasketballStatsApi.Core.Contracts;

public interface IGameService
{
  Task<GameResponse?> GetGameById(int gameId);
  
  Task<ICollection<GameResponse>> GetGamesByDate(GetGamesByDateRequest getGamesByDateRequest);

  Task<GameResponse?> CreateGame(CreateGameRequest createGameRequest);

  Task<GameResponse?> UpdateGameInfo(int gameId, UpdateGameInfoRequest updateGameInfoRequest);
  
  Task<GameResponse?> UpdateGameStatus(int gameId, UpdateGameStatusRequest updateGameStatusRequest);
  
  Task<GameResponse?> FinalizeGame(int gameId, FinalizeGameRequest finalizeGameRequest);
}