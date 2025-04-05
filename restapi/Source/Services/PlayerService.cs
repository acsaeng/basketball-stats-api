using AutoMapper;
using BasketballStatsApi.Core.Constants;
using BasketballStatsApi.Core.Contracts;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;
using BasketballStatsApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BasketballStatsApi.Services;

public class PlayerService(DataContext context, IMapper mapper) : IPlayerService
{
  public async Task<PlayerResponse?> GetPlayer(int playerId)
  {
    var player = await context.Players
      .Where(p => p.PlayerId == playerId)
      .Include(p => p.Team)
      .Include(p => p.GameStats)
        .ThenInclude(gs => gs.Game)
          .ThenInclude(g => g.HomeTeam)
      .Include(p => p.GameStats)
        .ThenInclude(gs => gs.Game)
          .ThenInclude(g => g.AwayTeam)
      .SingleOrDefaultAsync();

    if (player == null)
      return null;

    var response = mapper.Map<Player, PlayerResponse>(player);
    return response;
  }

  public async Task<ICollection<PlayerResponse>> GetLeagueLeaders(string statType)
  {
    if (statType is not 
          Validation.Player.StatTypes.Points or
          Validation.Player.StatTypes.Assists or
          Validation.Player.StatTypes.Rebounds or
          Validation.Player.StatTypes.Steals or
          Validation.Player.StatTypes.Blocks or
          Validation.Player.StatTypes.Turnovers
        )
      throw new ArgumentException(Error.Player.InvalidStatType);

    var players = await context.Players
      .OrderByDescending(p => EF.Property<object>(p, char.ToUpper(statType.First()) + statType.Substring(1)))
      .Take(5)
      .Include(p => p.Team)
      .Include(p => p.GameStats)
        .ThenInclude(gs => gs.Game)
          .ThenInclude(g => g.HomeTeam)
        .Include(p => p.GameStats)
          .ThenInclude(gs => gs.Game)
            .ThenInclude(g => g.AwayTeam)
      .ToListAsync();

    var response = mapper.Map<ICollection<Player>, ICollection<PlayerResponse>>(players);
    return response;
  }

  public async Task<PlayerResponse> CreatePlayer(CreatePlayerRequest request)
  {
    var player = mapper.Map<CreatePlayerRequest, Player>(request);
    context.Players.Add(player);
    await context.SaveChangesAsync();

    var response = mapper.Map<Player, PlayerResponse>(player);
    return response;
  }

  public async Task<PlayerResponse?> UpdatePlayerInfo(int playerId, UpdatePlayerInfoRequest request)
  {
    var player = await context.Players
      .Where(p => p.PlayerId == playerId)
      .Include(p => p.Team)
      .Include(p => p.GameStats)
        .ThenInclude(gs => gs.Game)
          .ThenInclude(g => g.HomeTeam)
      .Include(p => p.GameStats)
        .ThenInclude(gs => gs.Game)
          .ThenInclude(g => g.AwayTeam)
      .SingleOrDefaultAsync();

    if (player is null)
      return null;

    player.FirstName = request.FirstName;
    player.LastName = request.LastName;
    player.Dob = request.Dob;
    player.Height = request.Height;
    player.Weight = request.Weight;
    player.Position = request.Position;
    await context.SaveChangesAsync();

    var response = mapper.Map<Player, PlayerResponse>(player);
    return response;
  }

  public async Task<PlayerResponse?> UpdatePlayerInjury(int playerId, UpdatePlayerInjuryRequest request)
  {
    var player = await context.Players
      .Where(p => p.PlayerId == playerId)
      .Include(p => p.Team)
      .Include(p => p.GameStats)
        .ThenInclude(gs => gs.Game)
          .ThenInclude(g => g.HomeTeam)
      .Include(p => p.GameStats)
        .ThenInclude(gs => gs.Game)
          .ThenInclude(g => g.AwayTeam)
      .SingleOrDefaultAsync();

    if (player is null)
      return null;

    player.InjuryStatus = request.InjuryStatus;
    await context.SaveChangesAsync();

    var response = mapper.Map<Player, PlayerResponse>(player);
    return response;
  }

  public async Task<PlayerResponse?> UpdatePlayerRosterStatus(int playerId, UpdatePlayerRosterStatusRequest request)
  {
    var player = await context.Players
      .Where(p => p.PlayerId == playerId)
      .Include(p => p.Team)
      .Include(p => p.GameStats)
        .ThenInclude(gs => gs.Game)
          .ThenInclude(g => g.HomeTeam)
      .Include(p => p.GameStats)
        .ThenInclude(gs => gs.Game)
          .ThenInclude(g => g.AwayTeam)
      .SingleOrDefaultAsync();

    if (player is null)
      return null;

    player.RosterStatus = request.RosterStatus;
    player.TeamId = null;
    player.JerseyNumber = null;
    await context.SaveChangesAsync();

    var response = mapper.Map<Player, PlayerResponse>(player);
    return response;
  }
}