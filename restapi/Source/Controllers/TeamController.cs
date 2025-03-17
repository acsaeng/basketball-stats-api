using BasketballStatsApi.Core.Contracts;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BasketballStatsApi.Controllers;

// Endpoints to add:
// - Update team info
// - Add player to team
// - Deactivate team

[ApiController]
[Route("api/[controller]")]
public class TeamController(ITeamService teamService) : ControllerBase
{
  [HttpGet("{teamId}")]
  public async Task<ActionResult<TeamResponse>> GetTeam(int teamId)
  {
    var teamResponse = await teamService.GetTeam(teamId);

    if (teamResponse is null)
      return NotFound();

    return Ok(teamResponse);
  }

  [HttpPost]
  public async Task<ActionResult> AddTeam([FromBody] AddTeamRequest addTeamRequest)
  {
    var teamResponse = await teamService.AddTeam(addTeamRequest);
    return CreatedAtAction(nameof(GetTeam), new { teamId = teamResponse!.TeamId }, teamResponse);
  }

  [HttpPatch("{teamId}")]
  public async Task<ActionResult> UpdateTeam(int teamId, [FromBody] UpdateTeamRequest updateTeamRequest)
  {
    var team = await teamService.UpdateTeam(teamId, updateTeamRequest);

    if (team is null)
      return NotFound();

    return Ok(team);
  }
}