using AutoMapper;
using BasketballStatsApi.Core.Contracts;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;
using BasketballStatsApi.Infrastructure.Data;

namespace BasketballStatsApi.Services;

public class PlayerService(DataContext context, IMapper mapper) : IPlayerService
{
  public async Task<PlayerResponse?> GetPlayer(int playerId)
  {
    var player = await context.Players.FindAsync(playerId);
    var response = mapper.Map<PlayerResponse>(player);
    return response;
  }

  public async Task<PlayerResponse> AddPlayer(AddPlayerRequest addPlayerRequest)
  {
    var request = mapper.Map<Player>(addPlayerRequest);
    context.Players.Add(request);
    await context.SaveChangesAsync();

    var response = mapper.Map<PlayerResponse>(request);
    return response;
  }

  public async Task<PlayerResponse?> UpdatePlayerInfo(int playerId, UpdatePlayerInfoRequest updatePlayerInfoRequest)
  {
    var player = await context.Players.FindAsync(playerId);
    var request = mapper.Map<Player>(updatePlayerInfoRequest);

    if (player is null)
      return null;

    player.FirstName = request.FirstName;
    player.LastName = request.LastName;
    player.Dob = request.Dob;
    player.Height = request.Height;
    player.Weight = request.Weight;
    player.Position = request.Position;
    await context.SaveChangesAsync();

    var response = mapper.Map<PlayerResponse>(player);
    return response;
  }

  public async Task<PlayerResponse?> UpdatePlayerInjury(int playerId, UpdatePlayerInjuryRequest updatePlayerInjuryRequest)
  {
    var player = await context.Players.FindAsync(playerId);
    var request = mapper.Map<Player>(updatePlayerInjuryRequest);

    if (player is null)
      return null;

    player.InjuryStatus = request.InjuryStatus;
    await context.SaveChangesAsync();

    var response = mapper.Map<PlayerResponse>(player);
    return response;
  }

  public async Task<PlayerResponse?> UpdatePlayerTeam(int playerId, UpdatePlayerTeamRequest updatePlayerTeamRequest)
  {
    var player = await context.Players.FindAsync(playerId);
    var request = mapper.Map<Player>(updatePlayerTeamRequest);

    if (player is null)
      return null;

    player.RosterStatus = request.RosterStatus;
    player.JerseyNumber = request.JerseyNumber;
    await context.SaveChangesAsync();

    var response = mapper.Map<PlayerResponse>(player);
    return response;
  }
}