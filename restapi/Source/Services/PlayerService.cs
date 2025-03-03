using AutoMapper;
using BasketballStatsApi.Core.Contracts;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Entities;
using BasketballStatsApi.Core.Models;
using BasketballStatsApi.Infrastructure.Data;

namespace BasketballStatsApi.Services;

public class PlayerService(DataContext context, IMapper mapper) : IPlayerService
{
  public async Task<Player?> GetPlayer(int playerId)
  {
    var player = await context.Players.FindAsync(playerId);
    return player;
  }

  public async Task<Player> AddPlayer(Player player)
  {
    context.Players.Add(player);
    await context.SaveChangesAsync();
    return player;
  }

  public async Task UpdatePlayerInfo(int id, Player updatedPlayer)
  {
    var player = await context.Players.FindAsync(id);

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
    var updatedPlayer = mapper.Map<PlayerModel>(playerInjuryRequest);

    if (player is not null)
    {
      player.InjuryStatus = updatedPlayer.InjuryStatus;
      await context.SaveChangesAsync();
    }
  }

  public async Task UpdatePlayerTeam(int playerId, PlayerTeamRequest playerTeamRequest)
  {
    var player = await context.Players.FindAsync(playerId);
    var updatedPlayer = mapper.Map<PlayerModel>(playerTeamRequest);

    if (player is not null)
    {
      player.RosterStatus = updatedPlayer.RosterStatus;
      player.Team = updatedPlayer.Team;
      player.JerseyNumber = updatedPlayer.JerseyNumber;
      await context.SaveChangesAsync();
    }
  }
}