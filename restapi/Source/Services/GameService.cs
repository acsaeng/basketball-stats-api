using AutoMapper;
using BasketballStatsApi.Core.Constants;
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
        .ThenInclude(ps => ps.Player)
      .SingleOrDefaultAsync();

    if (game == null)
      return null;

    var response = mapper.Map<Game, GameResponse>(game);
    return response;
  }

  public async Task<ICollection<GameResponse>> GetGamesByDateRange(GetGamesByDateRangeRequest request)
  {
    var games = await context.Games
      .Where(g => DateOnly.FromDateTime(g.DateTime.Date) >= request.DateStart &&
                  DateOnly.FromDateTime(g.DateTime.Date) <= request.DateEnd)
      .Include(g => g.HomeTeam)
      .Include(g => g.AwayTeam)
      .Include(g => g.PlayerStats)
        .ThenInclude(ps => ps.Player)
      .ToListAsync();

    var response = mapper.Map<ICollection<Game>, ICollection<GameResponse>>(games);
    return response;
  }

  public async Task<GameResponse?> CreateGame(CreateGameRequest request)
  {
    if (request.DateTime.Date < DateTime.Today)
      throw new ArgumentOutOfRangeException(Error.Game.InvalidDate);

    var homeTeam = await context.Teams.FindAsync(request.HomeTeamId);
    var awayTeam = await context.Teams.FindAsync(request.AwayTeamId);

    if (homeTeam is null || awayTeam is null)
      throw new ArgumentNullException(Error.Game.TeamNotFound);

    if (homeTeam.Status == "Defunct" || awayTeam.Status == "Defunct")
     throw new InvalidOperationException(Error.Team.DefunctTeam);

    var game = mapper.Map<CreateGameRequest, Game>(request);
    context.Games.Add(game);
    await context.SaveChangesAsync();

    var response = mapper.Map<Game, GameResponse>(game);
    return response;
  }

  public async Task<GameResponse?> UpdateGameInfo(int gameId, UpdateGameInfoRequest request)
  {
    if (request.DateTime.Date < DateTime.Today)
      throw new ArgumentOutOfRangeException(Error.Game.InvalidDate);

    var game = await context.Games.FindAsync(gameId);

    if (game is null)
      return null;

    if (game.Status is not "Upcoming")
      throw new InvalidOperationException(Error.Game.InvalidState);

    var homeTeam = await context.Teams.FindAsync(request.HomeTeamId);
    var awayTeam = await context.Teams.FindAsync(request.AwayTeamId);

    if (homeTeam is null || awayTeam is null)
      throw new InvalidOperationException(Error.Game.TeamNotFound);
    
    if (homeTeam.Status == "Defunct" || awayTeam.Status == "Defunct")
      throw new InvalidOperationException(Error.Team.DefunctTeam);

    game.DateTime = request.DateTime;
    game.HomeTeamId = request.HomeTeamId;
    game.AwayTeamId = request.AwayTeamId;
    await context.SaveChangesAsync();

    var response = mapper.Map<Game, GameResponse>(game);
    return response;
  }

  public async Task<GameResponse?> UpdateGameStatus(int gameId, UpdateGameStatusRequest request)
  {
    var game = await context.Games.FindAsync(gameId);

    if (game is null)
      return null;

    if (game.Status is "Final" or "Cancelled")
      throw new InvalidOperationException(Error.Game.InvalidState);

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

    if (DateTime.Now < game.DateTime)
      throw new ArgumentOutOfRangeException(Error.Game.InvalidDate);

    if (game.Status is not "In progress")
      throw new InvalidOperationException(Error.Game.InvalidState);

    var homeTeamPoints = 0;
    var awayTeamPoints = 0;

    foreach (var playerStats in request.HomeTeamPlayerStats.Concat(request.AwayTeamPlayerStats))
    {
      var player = await context.Players
        .Where(p => p.PlayerId == playerStats.PlayerId)
        .Include(p => p.Team)
        .Include(p => p.GameStats)
        .SingleOrDefaultAsync();

      if (player is null)
        throw new ArgumentNullException(Error.Game.PlayerNotFound);

      if (player.TeamId != game.HomeTeamId && player.TeamId != game.AwayTeamId)
        throw new InvalidOperationException(Error.Game.InvalidPlayer);

      // Update player games stats
      var playerGameStats = mapper.Map<FinalizeGameRequestPlayerStats, PlayerGame>(
        playerStats,
        opt => opt.Items["GameId"] = game.GameId
      );
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

      if (player.TeamId == game.HomeTeamId)
        homeTeamPoints += playerGameStats.Points;
      else
        awayTeamPoints += playerGameStats.Points;
    }

    // Update team stats
    if (request.HomeTeamPoints != homeTeamPoints || request.AwayTeamPoints != awayTeamPoints)
      throw new InvalidOperationException(Error.Game.PointsNotEqual);

    if (homeTeamPoints == awayTeamPoints)
      throw new InvalidOperationException(Error.Game.TiesNotAllowed);

    var homeTeam = await context.Teams
      .Where(t => t.TeamId == game.HomeTeamId)
      .Include(t => t.Roster)
      .Include(t => t.HomeGames)
      .Include(t => t.AwayGames)
      .SingleOrDefaultAsync();
    var awayTeam = await context.Teams
      .Where(t => t.TeamId == game.AwayTeamId)
      .Include(t => t.Roster)
      .Include(t => t.HomeGames)
      .Include(t => t.AwayGames)
      .SingleOrDefaultAsync();

    if (homeTeam is null || awayTeam is null)
      throw new ArgumentNullException(Error.Game.TeamNotFound);

    if (game.HomeTeamPoints > game.AwayTeamPoints)
    {
      homeTeam.Wins += 1;
      awayTeam.Losses += 1;
      game.DidHomeTeamWin = true;
    }
    else
    {
      homeTeam.Losses += 1;
      awayTeam.Wins += 1;
      game.DidHomeTeamWin = false;
    }

    homeTeam.WinPercentage = homeTeam.Wins / (homeTeam.Wins + homeTeam.Losses);
    awayTeam.WinPercentage = awayTeam.Wins / (awayTeam.Wins + awayTeam.Losses);

    // Update game stats
    game.Status = "Final";
    game.HomeTeamPoints = request.HomeTeamPoints;
    game.AwayTeamPoints = request.AwayTeamPoints;
    await context.SaveChangesAsync();

    var response = mapper.Map<Game, GameResponse>(game);
    return response;
  }
}