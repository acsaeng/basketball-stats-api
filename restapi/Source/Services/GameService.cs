using AutoMapper;
using BasketballStatsApi.Core.Contracts;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;
using BasketballStatsApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BasketballStatsApi.Services;

public class GameService(DataContext context, IMapper mapper) : IGameService
{
  public async Task<GameResponse?> GetGame(int gameId)
  {
    var game = await context.Games
      .Where(g => g.GameId == gameId)
      .Include(g => g.HomeTeam)
      .Include(g => g.AwayTeam)
      .Include(g => g.PlayerStats)
      .SingleOrDefaultAsync();

    if (game == null)
      return null;

    var response = mapper.Map<Game, GameResponse>(game);
    return response;
  }

  public async Task<GameResponse?> CreateGame(CreateGameRequest createGameRequest)
  {
    var request = mapper.Map<CreateGameRequest, Game>(createGameRequest);

    var homeTeam = await context.Teams.FindAsync(request.HomeTeamId);
    var awayTeam = await context.Teams.FindAsync(request.AwayTeamId);

    if (homeTeam is null || awayTeam is null)
      return null;

    if (createGameRequest.DateTime.Date < DateTime.Today || homeTeam.Status == "Defunct" || awayTeam.Status == "Defunct")
      throw new InvalidOperationException();

    context.Games.Add(request);
    await context.SaveChangesAsync();

    var response = mapper.Map<Game, GameResponse>(request);
    return response;
  }
}