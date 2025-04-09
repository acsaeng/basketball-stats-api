using AutoMapper;
using BasketballStatsApi.Core.Constants;
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
      .Include(t => t.HomeGames)
        .ThenInclude(g => g.AwayTeam)
      .Include(t => t.AwayGames)
        .ThenInclude(g => g.HomeTeam)
      .SingleOrDefaultAsync();

    if (team is null)
      return null;

    var response = mapper.Map<Team, TeamResponse>(team);
    return response;
  }

  public async Task<ICollection<PlayerResponse>?> GetTeamRosterStats(int teamId)
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

  public async Task<ICollection<GameResponse>?> GetTeamSchedule(int teamId)
  {
    var team = await context.Teams.FindAsync(teamId);

    if (team is null)
      return null;

    var completedGames = await context.Games
      .Where(g => (team.TeamId == g.HomeTeamId || team.TeamId == g.AwayTeamId) && g.DateTime < DateTime.Now)
      .OrderByDescending(g => g.DateTime)
      .Take(5)
      .OrderBy(g => g.DateTime)
      .Include(g => g.AwayTeam)
      .ToListAsync();
    
    var upcomingGames = await context.Games
      .Where(g => (team.TeamId == g.HomeTeamId || team.TeamId == g.AwayTeamId) && g.DateTime > DateTime.Now)
      .OrderBy(g => g.DateTime)
      .Take(5)
      .Include(g => g.AwayTeam)
      .ToListAsync();

    var response = mapper.Map<ICollection<Game>, ICollection<GameResponse>>([.. completedGames, .. upcomingGames]);
    return response;
  }

  public async Task<ICollection<TeamResponse>> GetTeamStandings()
  {
    var teams = await context.Teams
      .Where(t => t.Status == Validation.Team.Status.Active)
      .OrderByDescending(t => t.WinPercentage)
      .Include(t => t.Roster)
      .Include(t => t.HomeGames)
        .ThenInclude(g => g.AwayTeam)
      .Include(t => t.AwayGames)
        .ThenInclude(g => g.HomeTeam)
      .ToListAsync();

    var response = mapper.Map<ICollection<Team>, ICollection<TeamResponse>>(teams);
    return response;
  }

  public async Task<TeamResponse> CreateTeam(CreateTeamRequest request)
  {
    var invalidTeams = await context.Teams
      .Where(t => t.Name == request.Name || t.Abbreviation == request.Abbreviation)
      .ToListAsync();

    if (invalidTeams.Count > 0)
      throw new InvalidOperationException(Error.Team.InvalidNameOrAbbr);

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
      .Include(t => t.HomeGames)
        .ThenInclude(g => g.AwayTeam)
      .Include(t => t.AwayGames)
        .ThenInclude(g => g.HomeTeam)
      .SingleOrDefaultAsync();

    if (team is null)
      return null;

    if (team.Status == Validation.Team.Status.Defunct)
      throw new InvalidOperationException(Error.Team.DefunctTeam);

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
    var team = await context.Teams
      .Where(t => t.TeamId == teamId)
      .Include(t => t.Roster)
      .Include(t => t.HomeGames)
        .ThenInclude(g => g.AwayTeam)
      .Include(t => t.AwayGames)
        .ThenInclude(g => g.HomeTeam)
      .SingleOrDefaultAsync();

    if (team is null)
      throw new InvalidOperationException(Error.Game.TeamNotFound);

    if (team.Status == Validation.Team.Status.Defunct)
      throw new InvalidOperationException(Error.Team.DefunctTeam);

    if (team.Roster.Count >= Validation.Team.MaxTeamRoster)
      throw new InvalidOperationException(Error.Team.MaxRosterExceeded);
    
    if (team.Roster.Any(p => p.JerseyNumber == request.JerseyNumber))
      throw new InvalidOperationException(Error.Team.InvalidJerseyNumber);

    var player = await context.Players.FindAsync(request.PlayerId);

    if (player is null)
      throw new InvalidOperationException(Error.Game.PlayerNotFound);

    player.RosterStatus = Validation.Player.RosterStatus.Active;
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
      .Include(t => t.HomeGames)
        .ThenInclude(g => g.AwayTeam)
      .Include(t => t.AwayGames)
        .ThenInclude(g => g.HomeTeam)
      .SingleOrDefaultAsync();

    if (team is null)
      return null;

    team.Status = Validation.Team.Status.Defunct;
    team.HeadCoach = null;

    // Cancel all future games
    var games = await context.Games
      .Where(g => g.Status == Validation.Game.Status.Upcoming && (g.HomeTeamId == team.TeamId || g.AwayTeamId == team.TeamId))
      .ToListAsync();

    foreach (var game in games)
      game.Status = Validation.Game.Status.Cancelled;

    // Remove all players from team
    var players = await context.Players
      .Where(player => player.TeamId == team.TeamId)
      .ToListAsync();

    foreach (var player in players)
    {
      player.RosterStatus = Validation.Player.RosterStatus.FreeAgent;
      player.TeamId = null;
      player.JerseyNumber = null;
    }

    await context.SaveChangesAsync();

    var response = mapper.Map<Team, TeamResponse>(team);
    return response;
  }
}