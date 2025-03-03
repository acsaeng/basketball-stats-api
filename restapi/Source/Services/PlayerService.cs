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
    var playerResponse = mapper.Map<PlayerResponse>(player);
    return playerResponse;
  }

  public async Task<PlayerResponse> AddPlayer(AddPlayerRequest addPlayerRequest)
  {
    var newPlayer = mapper.Map<Player>(addPlayerRequest);
    context.Players.Add(newPlayer);
    await context.SaveChangesAsync();
    var playerResponse = mapper.Map<PlayerResponse>(newPlayer);
    return playerResponse;
  }

  public async Task UpdatePlayerInfo(int id, UpdatePlayerInfoRequest updatePlayerInfoRequest)
  {
    var player = await context.Players.FindAsync(id);
    var updatedPlayer = mapper.Map<Player>(updatePlayerInfoRequest);

    if (player is not null)
    {
      player.FirstName = updatedPlayer.FirstName;
      player.LastName = updatedPlayer.LastName;
      player.Dob = updatedPlayer.Dob;
      player.Height = updatedPlayer.Height;
      player.Weight = updatedPlayer.Weight;
      player.Position = updatedPlayer.Position;
      await context.SaveChangesAsync();
    }
  }

  public async Task UpdatePlayerInjury(int playerId, PlayerInjuryRequest playerInjuryRequest)
  {
    var player = await context.Players.FindAsync(playerId);
    var updatedPlayer = mapper.Map<Player>(playerInjuryRequest);

    if (player is not null)
    {
      player.InjuryStatus = updatedPlayer.InjuryStatus;
      await context.SaveChangesAsync();
    }
  }

  public async Task UpdatePlayerTeam(int playerId, PlayerTeamRequest playerTeamRequest)
  {
    var player = await context.Players.FindAsync(playerId);
    var updatedPlayer = mapper.Map<Player>(playerTeamRequest);

    if (player is not null)
    {
      player.RosterStatus = updatedPlayer.RosterStatus;
      player.Team = updatedPlayer.Team;
      player.JerseyNumber = updatedPlayer.JerseyNumber;
      await context.SaveChangesAsync();
    }
  }
}