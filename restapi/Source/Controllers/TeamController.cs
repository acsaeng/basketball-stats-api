using BasketballStatsApi.Core.Contracts;
using BasketballStatsApi.Core.Dtos.Requests;
using BasketballStatsApi.Core.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BasketballStatsApi.Controllers;

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
  public async Task<ActionResult> CreateTeam([FromBody] CreateTeamRequest createTeamRequest)
  {
    var teamResponse = await teamService.CreateTeam(createTeamRequest);
    return CreatedAtAction(nameof(GetTeam), new { teamId = teamResponse!.TeamId }, teamResponse);
  }

  [HttpPost("update/{teamId}")]
  public async Task<ActionResult> UpdateTeam(int teamId, [FromBody] UpdateTeamRequest updateTeamRequest)
  {
    try
    {
      var teamResponse = await teamService.UpdateTeam(teamId, updateTeamRequest);

      if (teamResponse is null)
        return NotFound();
      
      return Ok(teamResponse);
    }
    catch (InvalidOperationException)
    {
      return BadRequest();
    }
  }

  [HttpPost("move-player/{teamId}")]
  public async Task<ActionResult> MovePlayerToTeam(int teamId, [FromBody] MovePlayerToTeam movePlayerToTeam)
  {
    try
    {
      var teamResponse = await teamService.MovePlayerToTeam(teamId, movePlayerToTeam);

      if (teamResponse is null)
        return NotFound();

      return Ok(teamResponse);
    }
    catch (InvalidOperationException)
    {
      return BadRequest();
    }
  }

  [HttpPost("deactivate/{teamId}")]
  public async Task<ActionResult> DeactivateTeam(int teamId)
  {
    var teamResponse = await teamService.DeactivateTeam(teamId);

    if (teamResponse is null)
      return NotFound();

    return Ok(teamResponse);
  }
}