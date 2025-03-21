using System.Collections;
using System.Globalization;
using AutoMapper;
using BasketballStatsApi.Core.Contracts;
using BasketballStatsApi.Core.Dtos.Helpers;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;
using BasketballStatsApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BasketballStatsApi.Services;

public class TeamService(DataContext context, IMapper mapper) : ITeamService
{
  public async Task<TeamResponse?> GetTeam(int teamId)
  {
    var team = await context.Teams.FindAsync(teamId);

    if (team is null)
      return null;

    var playersOnTeam = await context.Players
      .Where(e => e.TeamId == team.TeamId)
      .AsNoTracking()
      .ToListAsync();
    var players = mapper.Map<List<Player>, List<TeamPlayer>>(playersOnTeam);

    var response = mapper.Map<Team, TeamResponse>(team, opt => opt.Items["Players"] = players);
    return response;
  }

  public async Task<TeamResponse> AddTeam(AddTeamRequest addTeamRequest)
  {
    var request = mapper.Map<AddTeamRequest, Team>(addTeamRequest);
    context.Teams.Add(request);
    await context.SaveChangesAsync();

    var response = mapper.Map<Team, TeamResponse>(request, opt => opt.Items["Players"] = new List<TeamPlayer>());
    return response;
  }

  public async Task<TeamResponse?> UpdateTeam(int teamId, UpdateTeamRequest updateTeamRequest)
  {
    var team = await context.Teams.FindAsync(teamId);
    var request = mapper.Map<UpdateTeamRequest, Team>(updateTeamRequest);

    if (team is null)
      return null;

    // Cannot add players to an inactive team
    if (team.Status == "Defunct")
      throw new InvalidOperationException();

    team.Locale = request.Locale;
    team.Name = request.Name;
    team.Abbreviation = request.Abbreviation;
    team.Location = request.Location;
    team.Stadium = request.Stadium;
    await context.SaveChangesAsync();

    var players = await context.Players
      .Where(e => e.TeamId == team.TeamId)
      .AsNoTracking()
      .ToListAsync();

    var response = mapper.Map<Team, TeamResponse>(team, opt => opt.Items["Players"] = players);
    return response;
  }

  public async Task<TeamResponse?> AddPlayerToTeam(int teamId, AddPlayerToTeamRequest addPlayersToTeamRequest)
  {
    var request = mapper.Map<AddPlayerToTeamRequest, Player>(addPlayersToTeamRequest);
    var team = await context.Teams.FindAsync(teamId);
    var player = await context.Players.FindAsync(request.PlayerId);

    if (team is null || player is null)
      return null;

    // Cannot add player that is already on a team or if team is inactive
    if (player.TeamId is not null || team.Status == "Defunct")
      throw new InvalidOperationException();

    player.RosterStatus = "Active";
    player.TeamId = team.TeamId;
    player.JerseyNumber = request.JerseyNumber;
    await context.SaveChangesAsync();

    var players = await context.Players
      .Where(e => e.TeamId == team.TeamId)
      .AsNoTracking()
      .ToListAsync();

    var response = mapper.Map<Team, TeamResponse>(team, opt => opt.Items["Players"] = players);
    return response;
  }

  public async Task<TeamResponse?> DeactivateTeam(int teamId)
  {
    var team = await context.Teams.FindAsync(teamId);

    if (team is null)
      return null;

    team.Status = "Defunct";
    team.Wins = 0;
    team.Losses = 0;

    // Remove all players from team
    var players = await context.Players
      .Where(e => e.TeamId == team.TeamId)
      .ToListAsync();
    players.ForEach(player =>
    {
      player.RosterStatus = "Free agent";
      player.TeamId = null;
      player.JerseyNumber = null;
    });

    await context.SaveChangesAsync();

    var response = mapper.Map<Team, TeamResponse>(team, opt => opt.Items["Players"] = new List<TeamPlayer>());
    return response;
  }
}