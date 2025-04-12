using AutoMapper;
using BasketballLeagueApi.Core.Constants;
using BasketballLeagueApi.Core.Contracts;
using BasketballLeagueApi.Core.Dtos.Requests;
using BasketballLeagueApi.Core.Dtos.Responses;
using BasketballLeagueApi.Core.Entities;
using BasketballLeagueApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BasketballLeagueApi.Services;

public class PlayerService(DataContext context, IMapper mapper) : IPlayerService
{
  public async Task<PlayerResponse?> GetPlayer(int playerId)
  {
    var player = await context.Players
      .Where(p => p.PlayerId == playerId)
      .Include(p => p.Team)
      .SingleOrDefaultAsync();

    if (player is null)
      return null;

    var response = mapper.Map<Player, PlayerResponse>(player);
    return response;
  }

  public async Task<ICollection<PlayerResponse>> GetLeagueLeaders(string statType)
  {
    if (statType is not 
          (
            Validation.Player.StatTypes.Points or
            Validation.Player.StatTypes.Assists or
            Validation.Player.StatTypes.Rebounds or
            Validation.Player.StatTypes.Steals or
            Validation.Player.StatTypes.Blocks or
            Validation.Player.StatTypes.Turnovers
          )
        )
      throw new InvalidOperationException(Error.Player.InvalidStatType);

    var players = await context.Players
      .Where(p => p.RosterStatus != Validation.Player.RosterStatus.Retired)
      .OrderByDescending(p => EF.Property<object>(p, char.ToUpper(statType.First()) + statType.Substring(1)))
      .Take(5)
      .Include(p => p.Team)
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

  public async Task<PlayerResponse?> UpdatePlayerInjuryStatus(int playerId, UpdatePlayerInjuryRequest request)
  {
    var player = await context.Players
      .Where(p => p.PlayerId == playerId)
      .Include(p => p.Team)
      .SingleOrDefaultAsync();

    if (player is null)
      return null;
    
    if (player.RosterStatus is Validation.Player.RosterStatus.Retired)
      throw new InvalidOperationException(Error.Player.InactivePlayer);

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