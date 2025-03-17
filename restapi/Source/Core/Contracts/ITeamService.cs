using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;

namespace BasketballStatsApi.Core.Contracts;

public interface ITeamService
{
  Task<TeamResponse?> GetTeam(int teamId);

  Task<TeamResponse> AddTeam(AddTeamRequest addTeamRequest);

  Task<TeamResponse?> UpdateTeam(int teamId, UpdateTeamRequest updateTeamRequest);

  Task<TeamResponse?> DeactivateTeam(int teamId);
}