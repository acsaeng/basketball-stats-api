using AutoMapper;
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
    var player = await context.Players.FindAsync(playerId);

    if (player == null)
      return null;
    
    var teamAbbreviation = player.TeamId != null ? 
      await context.Teams
        .Where(team => team.TeamId == player.TeamId)
        .Select(team => team.Abbreviation)
        .SingleOrDefaultAsync()
      : null;

    var response = mapper.Map<Player, PlayerResponse>(player, opt => opt.Items["Team"] = teamAbbreviation);
    return response;
  }

  public async Task<PlayerResponse> CreatePlayer(CreatePlayerRequest createPlayerRequest)
  {
    var request = mapper.Map<CreatePlayerRequest, Player>(createPlayerRequest);
    context.Players.Add(request);
    await context.SaveChangesAsync();

    var response = mapper.Map<Player, PlayerResponse>(request, opt => opt.Items["Team"] = null);
    return response;
  }

  public async Task<PlayerResponse?> UpdatePlayerInfo(int playerId, UpdatePlayerInfoRequest updatePlayerInfoRequest)
  {
    var player = await context.Players.FindAsync(playerId);
    var request = mapper.Map<UpdatePlayerInfoRequest, Player>(updatePlayerInfoRequest);

    if (player is null)
      return null;

    player.FirstName = request.FirstName;
    player.LastName = request.LastName;
    player.Dob = request.Dob;
    player.Height = request.Height;
    player.Weight = request.Weight;
    player.Position = request.Position;
    await context.SaveChangesAsync();
    
    var teamAbbreviation = player.TeamId != null ? 
      await context.Teams
        .Where(team => team.TeamId == player.TeamId)
        .Select(team => team.Abbreviation)
        .SingleOrDefaultAsync()
      : null;

    var response = mapper.Map<Player, PlayerResponse>(player, opt => opt.Items["Team"] = teamAbbreviation);
    return response;
  }

  public async Task<PlayerResponse?> UpdatePlayerInjury(int playerId, UpdatePlayerInjuryRequest updatePlayerInjuryRequest)
  {
    var player = await context.Players.FindAsync(playerId);
    var request = mapper.Map<UpdatePlayerInjuryRequest, Player>(updatePlayerInjuryRequest);

    if (player is null)
      return null;

    player.InjuryStatus = request.InjuryStatus;
    await context.SaveChangesAsync();

    var teamAbbreviation = player.TeamId != null ? 
      await context.Teams
        .Where(team => team.TeamId == player.TeamId)
        .Select(team => team.Abbreviation)
        .SingleOrDefaultAsync()
      : null;

    var response = mapper.Map<Player, PlayerResponse>(player, opt => opt.Items["Team"] = teamAbbreviation);
    return response;
  }

  public async Task<PlayerResponse?> UpdatePlayerRosterStatus(int playerId, UpdatePlayerRosterStatusRequest updatePlayerRosterStatusRequest)
  {
    var player = await context.Players.FindAsync(playerId);
    var request = mapper.Map<UpdatePlayerRosterStatusRequest, Player>(updatePlayerRosterStatusRequest);

    if (player is null)
      return null;
    
    player.RosterStatus = request.RosterStatus;
    player.TeamId = null;
    player.JerseyNumber = null;
    await context.SaveChangesAsync();

    var response = mapper.Map<Player, PlayerResponse>(player, opt => opt.Items["Team"] = null);
    return response;
  }
}