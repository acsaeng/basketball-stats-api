using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Contracts;

public interface ITeamService
{
  Task<TeamResponse?> GetTeam(int teamId);

  Task<ICollection<TeamResponse>> GetTeamStandings();

  Task<TeamResponse> CreateTeam(CreateTeamRequest request);

  Task<TeamResponse?> UpdateTeam(int teamId, UpdateTeamRequest request);

  Task<TeamResponse?> AddPlayerToRoster(int teamId, AddPlayerToRosterRequest request);

  Task<TeamResponse?> DeactivateTeam(int teamId);
}