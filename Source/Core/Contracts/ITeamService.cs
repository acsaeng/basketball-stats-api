using BasketballStatsApi.Core.Dtos.Responses;

namespace BasketballStatsApi.Core.Contracts;

public interface ITeamService
{
  Task<TeamResponse?> GetTeam(int teamId);
}