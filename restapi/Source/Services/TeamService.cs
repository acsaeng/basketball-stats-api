using AutoMapper;
using BasketballStatsApi.Core.Contracts;
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
    var team = await context.Teams
      .Where(t => t.TeamId == teamId)
      .Include(t => t.Roster)
      .SingleOrDefaultAsync();

    if (team is null)
      return null;

    var response = mapper.Map<Team, TeamResponse>(team);
    return response;
  }

  public async Task<ICollection<PlayerResponse>?> GetTeamRoster(int teamId)
  {
    var team = await context.Teams.FindAsync(teamId);

    if (team is null)
      return null;

    var players = await context.Players
      .Where(p => p.TeamId == team.TeamId)
      .Include(p => p.Team)
      .ToListAsync();

    var response = mapper.Map<ICollection<Player>, ICollection<PlayerResponse>>(players);
    return response;
  }

  public async Task<TeamResponse> CreateTeam(CreateTeamRequest createTeamRequest)
  {
    var request = mapper.Map<CreateTeamRequest, Team>(createTeamRequest);
    context.Teams.Add(request);
    await context.SaveChangesAsync();

    var response = mapper.Map<Team, TeamResponse>(request);
    return response;
  }

  public async Task<TeamResponse?> UpdateTeam(int teamId, UpdateTeamRequest updateTeamRequest)
  {
    var team = await context.Teams
      .Where(t => t.TeamId == teamId)
      .Include(t => t.Roster)
      .SingleOrDefaultAsync();
    var request = mapper.Map<UpdateTeamRequest, Team>(updateTeamRequest);

    if (team is null)
      return null;

    // Cannot update inactive team
    if (team.Status == "Defunct")
      throw new InvalidOperationException();

    team.Locale = request.Locale;
    team.Name = request.Name;
    team.Abbreviation = request.Abbreviation;
    team.Location = request.Location;
    team.Stadium = request.Stadium;
    team.HeadCoach = request.HeadCoach;
    await context.SaveChangesAsync();

    var response = mapper.Map<Team, TeamResponse>(team);
    return response;
  }

  public async Task<TeamResponse?> AddPlayerToRoster(int teamId, AddPlayerToRosterRequest addPlayersToRosterRequest)
  {
    var request = mapper.Map<AddPlayerToRosterRequest, Player>(addPlayersToRosterRequest);
    var team = await context.Teams
      .Where(t => t.TeamId == teamId)
      .Include(t => t.Roster)
      .SingleOrDefaultAsync();
    var player = await context.Players.FindAsync(request.PlayerId);

    if (team is null || player is null)
      return null;

    // Cannot add player to inactive team
    if (team.Status == "Defunct")
      throw new InvalidOperationException();

    player.RosterStatus = "Active";
    player.TeamId = team.TeamId;
    player.JerseyNumber = request.JerseyNumber;
    await context.SaveChangesAsync();

    var response = mapper.Map<Team, TeamResponse>(team);
    return response;
  }

  public async Task<TeamResponse?> DeactivateTeam(int teamId)
  {
    var team = await context.Teams
      .Where(t => t.TeamId == teamId)
      .Include(t => t.Roster)
      .SingleOrDefaultAsync();

    if (team is null)
      return null;

    team.Status = "Defunct";
    team.HeadCoach = null;
    team.Wins = 0;
    team.Losses = 0;

    // Remove all players from team
    var players = await context.Players
      .Where(player => player.TeamId == team.TeamId)
      .ToListAsync();
    players.ForEach(player =>
    {
      player.RosterStatus = "Free agent";
      player.TeamId = null;
      player.JerseyNumber = null;
    });

    await context.SaveChangesAsync();

    var response = mapper.Map<Team, TeamResponse>(team);
    return response;
  }
}