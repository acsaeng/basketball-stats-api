using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using BasketballStatsApi.Core.Entities;

namespace BasketballStatsApi.Core.Contracts;

public interface ITeamService
{
  Task<TeamResponse?> GetTeam(int teamId);

  Task<ICollection<PlayerResponse>?> GetTeamRoster(int teamId);

  Task<TeamResponse> CreateTeam(CreateTeamRequest createTeamRequest);

  Task<TeamResponse?> UpdateTeam(int teamId, UpdateTeamRequest updateTeamRequest);

  Task<TeamResponse?> AddPlayerToRoster(int teamId, AddPlayerToRosterRequest addPlayersToRosterRequest);

  Task<TeamResponse?> DeactivateTeam(int teamId);
}