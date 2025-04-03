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

  public async Task<ICollection<TeamResponse>> GetTeamStandings()
  {
    var teams = await context.Teams
      .OrderByDescending(t => t.WinPercentage)
      .ToListAsync();
    return mapper.Map<ICollection<Team>, ICollection<TeamResponse>>(teams);
  }

  public async Task<TeamResponse> CreateTeam(CreateTeamRequest request)
  {
    var team = mapper.Map<CreateTeamRequest, Team>(request);
    context.Teams.Add(team);
    await context.SaveChangesAsync();

    var response = mapper.Map<Team, TeamResponse>(team);
    return response;
  }

  public async Task<TeamResponse?> UpdateTeam(int teamId, UpdateTeamRequest request)
  {
    var team = await context.Teams
      .Where(t => t.TeamId == teamId)
      .Include(t => t.Roster)
      .SingleOrDefaultAsync();

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

  public async Task<TeamResponse?> AddPlayerToRoster(int teamId, AddPlayerToRosterRequest request)
  {
    var updatedPlayer = mapper.Map<AddPlayerToRosterRequest, Player>(request);
    var team = await context.Teams
      .Where(t => t.TeamId == teamId)
      .Include(t => t.Roster)
      .SingleOrDefaultAsync();
    var player = await context.Players.FindAsync(updatedPlayer.PlayerId);

    if (team is null || player is null)
      return null;

    // Cannot add player to inactive team
    if (team.Status == "Defunct" || team.Roster.Count == 12)
      throw new InvalidOperationException();

    player.RosterStatus = "Active";
    player.TeamId = team.TeamId;
    player.JerseyNumber = updatedPlayer.JerseyNumber;
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

    // Cancel all team games
    var games = await context.Games
      .Where(g => g.Status == "Upcoming" && (g.HomeTeamId == team.TeamId || g.AwayTeamId == team.TeamId))
      .ToListAsync();

    foreach (var game in games)
      game.Status = "Cancelled";

    // Remove all players from team
    var players = await context.Players
      .Where(player => player.TeamId == team.TeamId)
      .ToListAsync();

    foreach (var player in players)
    {
      player.RosterStatus = "Free agent";
      player.TeamId = null;
      player.JerseyNumber = null;
    }

    await context.SaveChangesAsync();

    var response = mapper.Map<Team, TeamResponse>(team);
    return response;
  }
}