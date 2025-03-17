using AutoMapper;
using BasketballStatsApi.Core.Contracts;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;
using BasketballStatsApi.Infrastructure.Data;

namespace BasketballStatsApi.Services;

public class TeamService(DataContext context, IMapper mapper) : ITeamService
{
  public async Task<TeamResponse?> GetTeam(int teamId)
  {
    var team = await context.Teams.FindAsync(teamId);
    var response = mapper.Map<TeamResponse>(team);
    return response;
  }

  public async Task<TeamResponse> AddTeam(AddTeamRequest addTeamRequest)
  {
    var request = mapper.Map<Team>(addTeamRequest);
    context.Teams.Add(request);
    await context.SaveChangesAsync();

    var response = mapper.Map<TeamResponse>(request);
    return response;
  }

  public async Task<TeamResponse?> UpdateTeam(int teamId, UpdateTeamRequest updateTeamRequest)
  {
    var team = await context.Teams.FindAsync(teamId);
    var request = mapper.Map<Team>(updateTeamRequest);

    if (team is null)
      return null;

    team.Locale = request.Locale;
    team.Name = request.Name;
    team.Abbreviation = request.Abbreviation;
    team.Location = request.Location;
    team.Stadium = request.Stadium;
    await context.SaveChangesAsync();

    var response = mapper.Map<TeamResponse>(team);
    return response;
  }
}