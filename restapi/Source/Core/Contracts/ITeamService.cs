using BasketballLeagueApi.Core.Dtos.Requests;
using BasketballLeagueApi.Core.Dtos.Responses;

namespace BasketballLeagueApi.Core.Contracts;

public interface ITeamService
{
  Task<TeamResponse?> GetTeam(int teamId);

  Task<ICollection<PlayerResponse>?> GetTeamRosterStats(int teamId);

  Task<ICollection<GameResponse>?> GetTeamSchedule(int teamId);

  Task<ICollection<TeamResponse>> GetTeamStandings();

  Task<TeamResponse> CreateTeam(CreateTeamRequest request);

  Task<TeamResponse?> UpdateTeam(int teamId, UpdateTeamRequest request);

  Task<TeamResponse?> AddPlayerToRoster(int teamId, AddPlayerToRosterRequest request);

  Task<TeamResponse?> DeactivateTeam(int teamId);
}