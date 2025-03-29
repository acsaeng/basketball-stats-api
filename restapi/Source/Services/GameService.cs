using AutoMapper;
using BasketballStatsApi.Core.Contracts;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;
using BasketballStatsApi.Core.Models;
using BasketballStatsApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BasketballStatsApi.Services;

public class GameService(DataContext context, IMapper mapper) : IGameService
{
  public async Task<GameResponse?> GetGameById(int gameId)
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
  
  public async Task<ICollection<GameResponse>> GetGamesByDate(GetGamesByDateRequest getGamesByDateRequest)
  {
    var request = mapper.Map<GetGamesByDateRequest, Game>(getGamesByDateRequest);
    
    var games = await context.Games
      .Where(g => g.DateTime.Date == request.DateTime.Date)
      .Include(g => g.HomeTeam)
      .Include(g => g.AwayTeam)
      .Include(g => g.PlayerStats)
      .ToListAsync();

    var response = mapper.Map<ICollection<Game>, ICollection<GameResponse>>(games);
    return response;
  }

  public async Task<GameResponse?> CreateGame(CreateGameRequest createGameRequest)
  {
    var request = mapper.Map<CreateGameRequest, Game>(createGameRequest);
    var homeTeam = await context.Teams.FindAsync(request.HomeTeamId);
    var awayTeam = await context.Teams.FindAsync(request.AwayTeamId);

    if (homeTeam is null || awayTeam is null)
      return null;

    if (request.DateTime.Date < DateTime.Today || homeTeam.Status == "Defunct" || awayTeam.Status == "Defunct")
      throw new InvalidOperationException();

    context.Games.Add(request);
    await context.SaveChangesAsync();

    var response = mapper.Map<Game, GameResponse>(request);
    return response;
  }

  public async Task<GameResponse?> UpdateGameInfo(int gameId, UpdateGameInfoRequest updateGameInfoRequest)
  {
    var game = await context.Games.FindAsync(gameId);
    var request = mapper.Map<UpdateGameInfoRequest, Game>(updateGameInfoRequest);
    var homeTeam = await context.Teams.FindAsync(request.HomeTeamId);
    var awayTeam = await context.Teams.FindAsync(request.AwayTeamId);

    if (game is null || homeTeam is null || awayTeam is null)
      return null;

    // TODO: Throw ArgumentOutOfRangeException for first condition
    if (request.DateTime.Date < DateTime.Today ||
        game.Status != "Upcoming" ||
        homeTeam.Status == "Defunct" ||
        awayTeam.Status == "Defunct")
      throw new InvalidOperationException();
    
    game.DateTime = request.DateTime;
    game.HomeTeamId = request.HomeTeamId;
    game.AwayTeamId = request.AwayTeamId;
    await context.SaveChangesAsync();

    var response = mapper.Map<Game, GameResponse>(game);
    return response;
  }
  
  public async Task<GameResponse?> UpdateGameStatus(int gameId, UpdateGameStatusRequest updateGameStatusRequest)
  {
    var game = await context.Games.FindAsync(gameId);
    var request = mapper.Map<UpdateGameStatusRequest, Game>(updateGameStatusRequest);

    if (game is null)
      return null;

    if (game.Status is "Final" or "Cancelled")
      throw new InvalidOperationException();
    
    game.Status = request.Status;
    await context.SaveChangesAsync();

    var response = mapper.Map<Game, GameResponse>(game);
    return response;
  }

  public async Task<GameResponse?> FinalizeGame(int gameId, FinalizeGameRequest request)
  {
    var game = await context.Games
      .Where(g => g.GameId == gameId)
      .Include(g => g.HomeTeam)
      .Include(g => g.AwayTeam)
      .Include(g => g.PlayerStats)
      .SingleOrDefaultAsync();

    if (game is null)
      return null;
    
    if (game.Status is not "In progress")
      throw new InvalidOperationException();
    
    var pointsHome = 0;
    var pointsAway = 0;
    
    foreach (var stats in request.HomeTeamPlayerStats.Concat(request.AwayTeamPlayerStats))
    {
      var player = await context.Players
        .Where(p => p.PlayerId == stats.PlayerId)
        .Include(p => p.Team)
        .Include(p => p.GameStats)
        .SingleOrDefaultAsync();

      if (player is null)
        throw new InvalidOperationException();
      
      // Update player games stats
      var playerGameStats = mapper.Map<FinalizeGamePlayerStats, PlayerGameStats>(
        stats,
        opt =>
        {
          opt.Items["Player"] = player;
          opt.Items["Game"] = game;
        });
      player.GameStats.Add(playerGameStats);
      
      // Update individual player stats
      var playerModel = mapper.Map<Player, PlayerModel>(player);
      playerModel.AddGamesStats(playerGameStats);
      
      player.GamesPlayed = playerModel.GamesPlayed;
      player.Points = playerModel.Points;
      player.Assists = playerModel.Assists;
      player.Rebounds = playerModel.Rebounds;
      player.Steals = playerModel.Steals;
      player.Blocks = playerModel.Blocks;
      player.Turnovers = playerModel.Turnovers;
      
      if (player.Team == game.HomeTeam)
        pointsHome += playerGameStats.Points;
      else
        pointsAway += playerGameStats.Points;
    }
    
    if (request.PointsHome != pointsHome || request.PointsAway != pointsAway)
      throw new InvalidOperationException();
    
    // Update team stats
    var homeTeam = await context.Teams
      .Where(t => t == game.HomeTeam)
      .Include(t => t.Roster)
      .Include(t => t.Games)
      .SingleOrDefaultAsync();
    var awayTeam = await context.Teams
      .Where(t => t == game.AwayTeam)
      .Include(t => t.Roster)
      .Include(t => t.Games)
      .SingleOrDefaultAsync();

    if (game.PointsHome > game.PointsAway)
    {
      homeTeam.Wins += 1;
      awayTeam.Losses += 1;
    }
    else
    {
      homeTeam.Losses += 1;
      awayTeam.Wins += 1;
    }
    
    // Update game stats
    game.Status = "Final";
    game.PointsHome = request.PointsHome;
    game.PointsAway = request.PointsAway;
    await context.SaveChangesAsync();

    var response = mapper.Map<Game, GameResponse>(game);
    return response;
  }
}