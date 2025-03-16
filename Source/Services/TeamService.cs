using AutoMapper;
using BasketballStatsApi.Core.Contracts;
using BasketballStatsApi.Core.Dtos.Responses;
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
}